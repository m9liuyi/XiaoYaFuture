using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiaoYaFuture.Common;
using XiaoYaFuture.DataAcessLayer.Interface;
using XiaoYaFuture.QueryParameter;

namespace XiaoYaFuture.DataAcessLayer
{
    /// <summary>
    /// 提供基本的数据库操作
    /// </summary>
    /// <typeparam name="T">DTO</typeparam>
    /// <typeparam name="E">Entity</typeparam>
    public class BaseRepository<T, E> : IBaseRepository<T, E> 
        where T : BaseDto 
        where E : BaseEntity
    {
        public IBaseDal<T, E> dal;

        public BaseRepository(IBaseDal<T, E> dal)
        {
            this.dal = dal;
        }

        #region IBaseRepository<T> Members

        public T GetById(int id)
        {
            return this.dal.List(new BaseQueryParameters() {
                PrimaryKey = id
            }).FirstOrDefault<T>();
        }

        public List<T> List<M>(M query)
            where M : BaseQueryParameters
        {
            return this.dal.List<M>(query).ToList<T>();
        }

        public T Insert(T entity)
        {
            var results = this.dal.BulkInsert(new List<T>() { entity });
            return results.FirstOrDefault();
        }

        public T Update(T entity)
        {
            var results = this.dal.BulkUpdate(new List<T>() { entity });
            return results.FirstOrDefault();
        }

        public List<T> Insert(List<T> entities)
        {
            return this.dal.BulkInsert(entities);
        }

        public List<T> Update(List<T> entities)
        {
            return this.dal.BulkUpdate(entities);
        }

        public T Remove(T entity)
        {
            var results = this.dal.BulkRemove(new List<T>() { entity });
            return results.FirstOrDefault();
        }

        public T Remove(int id)
        {
            var results = this.dal.BulkRemoveById(new BaseQueryParameters()
            {
                PrimaryKey = id,
            });
            return results.FirstOrDefault();
        }

        public List<T> Remove(List<T> entities)
        {
            return this.dal.BulkRemove(entities);
        }

        public List<T> Remove(List<int> ids)
        {
            var results = this.dal.BulkRemoveById(new BaseQueryParameters()
            {
                PrimaryKeys = ids,
            });
            return results;
        }

        public List<T> Delete(List<int> ids)
        {
            var results = this.dal.BulkDeleteById(new BaseQueryParameters()
            {
                PrimaryKeys = ids,
            });
            return results;
        }

        public List<T> Delete(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Delete(int id)
        {
            var results = this.dal.BulkDeleteById(new BaseQueryParameters()
            {
                PrimaryKey = id,
            });
            return results.FirstOrDefault();
        }
        #endregion
    }
}
