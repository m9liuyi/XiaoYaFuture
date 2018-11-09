using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NiceNet.Common;
using NiceNet.QueryParameter;

namespace NiceNet.DataAcessLayer.Interface
{
    public interface IBaseDal<E> where E : BaseEntity
    {
        List<E> List<M>(M query) where M : BaseQueryParameters;

        List<E> BulkInsert(List<E> entities);

        List<E> BulkUpdate(List<E> entities);

        /// <summary>
        /// 此方法 query 中只允许使用PrimaryKey(s)
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="query"></param>
        /// <param name="columnValues"></param>
        /// <returns></returns>
        List<E> BulkUpdate<M>(M query, Dictionary<string, object> columnValues) where M : BaseQueryParameters;

        List<E> BulkRemove(List<E> entities);

        /// <summary>
        /// 此方法 query 中只允许使用PrimaryKey(s)
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        List<E> BulkRemoveById<M>(M query) where M : BaseQueryParameters;

        List<E> BulkDelete(List<E> entities);

        /// <summary>
        /// 此方法 query 中只允许使用PrimaryKey(s)
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        List<E> BulkDeleteById<M>(M query) where M : BaseQueryParameters;
    }
}
