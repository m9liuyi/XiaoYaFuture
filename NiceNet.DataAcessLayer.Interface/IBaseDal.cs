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
        /// <summary>
        /// 根据传入的 QueryParameter 查询
        /// </summary>
        List<E> List<M>(M query) where M : BaseQueryParameters;

        /// <summary>
        /// 批量插入
        /// </summary>
        List<E> BulkInsert(List<E> entities);

        /// <summary>
        /// 批量更新(整个Entity)
        /// </summary>
        List<E> BulkUpdate(List<E> entities);

        /// <summary>
        /// 更新根据 query.PrimaryKey(s) 找到的记录的 columnValues
        /// </summary>
        /// <param name="columnValues">字段名,值</param>
        List<E> BulkUpdate<M>(M query, Dictionary<string, object> columnValues) where M : BaseQueryParameters;

        /// <summary>
        /// 批量物理删除
        /// </summary>
        List<E> BulkRemove(List<E> entities);

        /// <summary>
        /// 批量物理删除根据 query.PrimaryKey(s) 找到的记录
        /// </summary>
        List<E> BulkRemoveById<M>(M query) where M : BaseQueryParameters;

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        List<E> BulkDelete(List<E> entities);

        /// <summary>
        /// 批量逻辑删除根据 query.PrimaryKey(s) 找到的记录
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        List<E> BulkDeleteById<M>(M query) where M : BaseQueryParameters;
    }
}
