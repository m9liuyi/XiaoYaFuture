using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiaoYaFuture.Common;
using XiaoYaFuture.QueryParameter;

namespace XiaoYaFuture.DataAcessLayer.Interface
{
    public interface IBaseRepository<T, E> 
        where T : BaseDto
        where E : BaseEntity
    {
        T GetById(int id);

        List<T> List<M>(M query) where M : BaseQueryParameters;

        T Insert(T entity);
        T Update(T entity);
        T Remove(T entity);
        T Remove(int id);
        T Delete(T entity);
        T Delete(int id);

        List<T> Insert(List<T> entities);
        List<T> Update(List<T> entities);
        List<T> Remove(List<T> entities);
        List<T> Remove(List<int> ids);
        List<T> Delete(List<T> entities);
        List<T> Delete(List<int> ids);
    }
}
