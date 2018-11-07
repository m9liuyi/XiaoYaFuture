using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceNet.Common;
using NiceNet.DataAcessLayer.Interface;
using NiceNet.QueryParameter;

namespace NiceNet.DataAcessLayer
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
        public IBaseDal<E> dal;

        public BaseRepository(IBaseDal<E> dal)
        {
            this.dal = dal;
        }

        #region IBaseRepository<T> Members

        public T GetById(int id)
        {
            return this.dal.List(new BaseQueryParameters() {
                PrimaryKey = id
            })
            .S2T<E, T>()
            .FirstOrDefault<T>();
        }

        public List<T> List<M>(M query)
            where M : BaseQueryParameters
        {
            return this.dal.List<M>(query)
                .S2T<E, T>();
        }

        public T Insert(T dto)
        {
            var results = this.dal.BulkInsert(new List<E>() { dto.S2T<T, E>() });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public T Update(T dto)
        {
            var results = this.dal.BulkUpdate(new List<E>() { dto.S2T<T, E>() });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public List<T> Insert(List<T> dtos)
        {
            return this.dal.BulkInsert(dtos.S2T<T, E>())
                .S2T<E, T>();
        }

        public List<T> Update(List<T> dtos)
        {
            return this.dal.BulkUpdate(dtos.S2T<T, E>())
                .S2T<E, T>();
        }

        public List<T> Update(List<int> ids, Dictionary<string, object> columnValues)
        {
            return this.dal.BulkUpdate(new BaseQueryParameters()
            {
                PrimaryKeys = ids,
            }, columnValues)
            .S2T<E, T>();
        }

        public T Remove(T dto)
        {
            var results = this.dal.BulkRemove(new List<E>() { dto.S2T<T, E>() });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public T Remove(int id)
        {
            var results = this.dal.BulkRemoveById(new BaseQueryParameters()
            {
                PrimaryKey = id,
            });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public List<T> Remove(List<T> dtos)
        {
            return this.dal.BulkRemove(dtos.S2T<T, E>()).S2T<E, T>();
        }

        public List<T> Remove(List<int> ids)
        {
            var results = this.dal.BulkRemoveById(new BaseQueryParameters()
            {
                PrimaryKeys = ids,
            });
            return results.S2T<E, T>();
        }

        public List<T> Delete(List<int> ids)
        {
            var results = this.dal.BulkDeleteById(new BaseQueryParameters()
            {
                PrimaryKeys = ids,
            });
            return results.S2T<E, T>();
        }

        public List<T> Delete(List<T> dtos)
        {
            return this.dal.BulkDelete(dtos.S2T<T, E>())
                .S2T<E, T>();
        }

        public T Delete(T dto)
        {
            return this.dal.BulkDelete(new List<E>() { dto.S2T<T, E>() })
                .S2T<E, T>()
                .FirstOrDefault();
        }

        public T Delete(int id)
        {
            var results = this.dal.BulkDeleteById(new BaseQueryParameters()
            {
                PrimaryKey = id,
            });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public List<T> GetByIds(List<int> ids)
        {
            return this.dal.List(new BaseQueryParameters()
            {
                PrimaryKeys = ids
            })
            .S2T<E, T>();
        }
        #endregion
    }
}
