using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XiaoYaFuture.Common;
using XiaoYaFuture.QueryParameter;

namespace XiaoYaFuture.DataAcessLayer.Interface
{
    public interface IBaseDal<T, E> 
        where T : BaseDto
        where E : BaseEntity
    {
        /// <summary>
        /// 获取IQueryable数据源
        /// </summary>
        /// <typeparam name="M">BaseQueryParameters</typeparam>
        /// <param name="query">BaseQueryParameters子类</param>
        /// <returns></returns>
        List<T> List<M>(M query) where M : BaseQueryParameters;

        List<T> BulkInsert(List<T> entities);

        List<T> BulkUpdate(List<T> entities);

        List<T> BulkRemove(List<T> entities);

        List<T> BulkRemoveById<M>(M query) where M : BaseQueryParameters;

        List<T> BulkDelete(List<T> entities);

        List<T> BulkDeleteById<M>(M query) where M : BaseQueryParameters;
    }
}
