using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NiceNet.QueryParameter;
using Z.BulkOperations;

namespace NiceNet.Common
{
    public static class Extension
    {

        /// <summary>
        /// 将Query转化为where条件Lamada表达式
        /// </summary>
        /// <typeparam name="E">Entity</typeparam>
        /// <typeparam name="M">BaseQueryParameters</typeparam>
        /// <param name="queryParameter"></param>
        /// <returns></returns>
        public static Expression<Func<E, bool>> QueryToWhere<E, M>(M queryParameter)
            where E : BaseEntity
            where M : BaseQueryParameters
        {

            if (!(queryParameter is BaseQueryParameters))
            {
                throw new ArgumentException("Extension.QueryToWhere: 参数必须是 BaseQueryParameters 的子类");
            }

            // 表达式中的变量
            ParameterExpression variable = Expression.Parameter(typeof(E), "fff");
            // 表达式中的值
            Expression expression = Expression.Constant(true);

            Type t = queryParameter.GetType();
            PropertyInfo[] propertyList = t.GetProperties();
            foreach (PropertyInfo item in propertyList)
            {
                string fieldName = item.Name;

                object value = item.GetValue(queryParameter, null);
                if (value != null)
                {
                    var type = item.PropertyType;
                    if (type == typeof(string))
                    {
                        if (fieldName.EndsWith("Equals"))
                        {
                            Expression rightExpress = GetMethodCallExpression<E>(true, variable, fieldName, type, "Equals", value);
                            expression = Expression.And(expression, rightExpress);
                        }
                        else if (fieldName.EndsWith("Contains"))
                        {
                            Expression rightExpress = GetMethodCallExpression<E>(true, variable, fieldName, type, "Contains", value);
                            expression = Expression.And(expression, rightExpress);
                        }
                    }
                    else if (type == typeof(int?) && fieldName == "PrimaryKey")
                    {
                        // 获取主键属性
                        var primaryKey = typeof(E).GetProperties().Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute))).FirstOrDefault();

                        Expression rightExpress = GetMethodCallExpression<E>(true, variable, primaryKey.Name, type.GetGenericArguments()[0], "Equals", value);
                        expression = Expression.And(expression, rightExpress);
                    }
                    else if (type == typeof(List<int>) && fieldName == "PrimaryKeys")
                    {
                        // 获取主键属性
                        var primaryKey = typeof(E).GetProperties().Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute))).FirstOrDefault();

                        Expression rightExpress = GetMethodCallExpression<E>(false, variable, primaryKey.Name, type.GetGenericArguments()[0], "Contains", value);
                        expression = Expression.And(expression, rightExpress);
                    }
                    else if (type == typeof(int?) || type == typeof(DateTime?) || type == typeof(decimal?) || type == typeof(long?) || type == typeof(double?))
                    {
                        if (fieldName.EndsWith("Begin") || fieldName.EndsWith("Min"))
                        {
                            Expression rightExpress = GetMethodCallExpression<E>(true, variable, fieldName, type.GetGenericArguments()[0], ">=", value);
                            expression = Expression.And(expression, rightExpress);
                        }
                        else if (fieldName.EndsWith("End") || fieldName.EndsWith("Max"))
                        {
                            Expression rightExpress = GetMethodCallExpression<E>(true, variable, fieldName, type.GetGenericArguments()[0], "<=", value);
                            expression = Expression.And(expression, rightExpress);
                        }
                        else
                        {
                            Expression rightExpress = GetMethodCallExpression<E>(true, variable, fieldName, type.GetGenericArguments()[0], "Equals", value);
                            expression = Expression.And(expression, rightExpress);
                        }
                    }
                    else if (type == typeof(List<int>) || type == typeof(List<long>) || type == typeof(List<decimal>) || type == typeof(List<double>) || type == typeof(List<string>))
                    {
                        Expression rightExpress = GetMethodCallExpression<E>(false, variable, fieldName, type, "Equals", value);
                        expression = Expression.And(expression, rightExpress);
                    }
                    else
                    {
                        throw new NotSupportedException("Extension.QueryToWhere: 查询参数的数据类型不支持");
                    }
                }
            }

            Expression<Func<E, bool>> finalExpression
                = Expression.Lambda<Func<E, bool>>(expression, new ParameterExpression[] { variable });

            return finalExpression;
        }

        private static MethodCallExpression GetMethodCallExpression<E>(bool isLeftVar, ParameterExpression left, string fieldName, Type fieldType, string operation, object value)
        {
            if (isLeftVar)
            {
                return Expression.Call(
                                    Expression.Property(left, typeof(E).GetProperty(fieldName)),
                                    fieldType.GetRuntimeMethod(operation, new Type[] { value.GetType() }),
                                    Expression.Constant(value));
            }
            else
            {
                return Expression.Call(
                                    Expression.Constant(value),
                                    value.GetType().GetMethod(operation, new Type[] { fieldType }),
                                    Expression.Property(left, typeof(E).GetProperty(fieldName)));
            }
        }

        public static List<T> AuditEntryToEntity<T>(this List<AuditEntry> auditEntries) where T : class
        {
            if (auditEntries == null || auditEntries.Count == 0)
            {
                return new List<T>();
            }

            var type = typeof(T);
            return auditEntries.Select(p =>
            {

                T obj = (T)type.Assembly.CreateInstance(type.FullName);

                p.Values.ForEach(q =>
                {
                    string columnName = q.ColumnName;
                    object columnValue = q.NewValue;

                    PropertyInfo property = type.GetProperty(columnName);
                    if (property != null)
                    {
                        if (columnValue != DBNull.Value && columnValue != null)
                        {
                            if (property.PropertyType.IsEnum)
                            {
                                object enumName = Enum.ToObject(property.PropertyType, columnValue);
                                property.SetValue(obj, enumName, null);
                            }
                            else
                            {
                                property.SetValue(obj, columnValue, null);
                            }
                        }
                    }
                });
                return obj;
            }).ToList<T>();
        }

        /// <summary>
        /// Source => Target
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<T> S2T<S, T>(this List<S> sourceList)
        {
            if (sourceList == null || sourceList.Count == 0)
            {
                return new List<T>();
            }

            var targetType = typeof(T);
            var sourceType = typeof(S);

            var propertiesOfTarget = targetType.GetProperties();
            var propertiesOfSource = sourceType.GetProperties();

            var propertyNamesOfTarget = propertiesOfTarget.Select(p => p.Name).ToList();
            var propertyNamesOfSource = propertiesOfSource.Select(p => p.Name).ToList();

            if (propertiesOfSource.Where(p => !propertyNamesOfTarget.Contains(p.Name)).Any())
            {
                throw new Exception("S中有些属性在T中不存在");
            }

            
            return sourceList.Select(p =>
            {
                T obj = (T)targetType.Assembly.CreateInstance(targetType.FullName);

                foreach (var columnName in propertyNamesOfSource)
                {
                    object columnValue = sourceType.GetProperty(columnName).GetValue(p);

                    PropertyInfo property = targetType.GetProperty(columnName);
                    if (columnValue != DBNull.Value && columnValue != null)
                    {
                        if (property.PropertyType.IsEnum)
                        {
                            object enumName = Enum.ToObject(property.PropertyType, columnValue);
                            property.SetValue(obj, enumName, null);
                        }
                        else
                        {
                            property.SetValue(obj, columnValue, null);
                        }
                    }
                }

                return obj;
            })
            .ToList<T>();
        }


        public static T S2T<S, T>(this S source)
        {
            if (source == null)
            {
                return default(T);
            }

            var targetType = typeof(T);
            var sourceType = typeof(S);

            var propertiesOfTarget = targetType.GetProperties();
            var propertiesOfSource = sourceType.GetProperties();

            var propertyNamesOfTarget = propertiesOfTarget.Select(p => p.Name).ToList();
            var propertyNamesOfSource = propertiesOfSource.Select(p => p.Name).ToList();

            if (propertiesOfSource.Where(p => !propertyNamesOfTarget.Contains(p.Name)).Any())
            {
                throw new Exception("S中有些属性在T中不存在");
            }



            T obj = (T)targetType.Assembly.CreateInstance(targetType.FullName);

            foreach (var columnName in propertyNamesOfSource)
            {
                object columnValue = sourceType.GetProperty(columnName).GetValue(source);

                PropertyInfo property = targetType.GetProperty(columnName);
                if (columnValue != DBNull.Value && columnValue != null)
                {
                    if (property.PropertyType.IsEnum)
                    {
                        object enumName = Enum.ToObject(property.PropertyType, columnValue);
                        property.SetValue(obj, enumName, null);
                    }
                    else
                    {
                        property.SetValue(obj, columnValue, null);
                    }
                }
            }

            return obj;
        }


        public static T PrepareEntity4UpdateFromQuery<T>(Dictionary<string, object> keyValues) 
            where T : BaseEntity
        {
            var targetType = typeof(T);

            var returnObj = targetType.Assembly.CreateInstance(targetType.FullName);
            foreach (var one in keyValues)
            {
                PropertyInfo property = targetType.GetProperty(one.Key);

                if (one.Value != DBNull.Value && one.Value != null)
                {
                    if (property.PropertyType.IsEnum)
                    {
                        object enumName = Enum.ToObject(property.PropertyType, one.Value);
                        property.SetValue(returnObj, enumName, null);
                    }
                    else
                    {
                        property.SetValue(returnObj, one.Value, null);
                    }
                }
            }

            return (T)returnObj;
        }
    }
}
