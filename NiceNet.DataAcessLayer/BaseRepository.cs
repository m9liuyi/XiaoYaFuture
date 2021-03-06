﻿using System;
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
        public IBaseDal<E> Dal { get; set; }

        #region IBaseRepository<T> Members

        public T GetById(int id)
        {
            return this.Dal.List(new BaseQueryParameters()
            {
                PrimaryKey = id
            })
            .S2T<E, T>()
            .FirstOrDefault<T>();
        }

        public List<T> GetByIds(List<int> ids)
        {
            return this.Dal.List(new BaseQueryParameters()
            {
                PrimaryKeys = ids
            })
            .S2T<E, T>();
        }

        public List<T> List<M>(M query) where M : BaseQueryParameters
        {
            return this.Dal.List<M>(query)
                .S2T<E, T>();
        }

        public T InsertDto(T dto)
        {
            var results = this.Dal.BulkInsert(new List<E>() { dto.S2T<T, E>() });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public T UpdateDto(T dto)
        {
            var results = this.Dal.BulkUpdate(new List<E>() { dto.S2T<T, E>() });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public List<T> InsertDtos(List<T> dtos)
        {
            return this.Dal.BulkInsert(dtos.S2T<T, E>())
                .S2T<E, T>();
        }

        public List<T> UpdateDtos(List<T> dtos)
        {
            return this.Dal.BulkUpdate(dtos.S2T<T, E>())
                .S2T<E, T>();
        }

        public List<T> UpdateProperties(List<int> ids, Dictionary<string, object> columnValues)
        {
            return this.Dal.BulkUpdate(new BaseQueryParameters()
            {
                PrimaryKeys = ids,
            }, columnValues)
            .S2T<E, T>();
        }

        public T RemoveDto(T dto)
        {
            var results = this.Dal.BulkRemove(new List<E>() { dto.S2T<T, E>() });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public List<T> RemoveDtos(List<T> dtos)
        {
            return this.Dal.BulkRemove(dtos.S2T<T, E>()).S2T<E, T>();
        }

        public T RemoveById(int id)
        {
            var results = this.Dal.BulkRemoveById(new BaseQueryParameters()
            {
                PrimaryKey = id,
            });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public List<T> RemoveByIds(List<int> ids)
        {
            var results = this.Dal.BulkRemoveById(new BaseQueryParameters()
            {
                PrimaryKeys = ids,
            });
            return results.S2T<E, T>();
        }

        public T DeleteDto(T dto)
        {
            return this.Dal.BulkDelete(new List<E>() { dto.S2T<T, E>() })
                .S2T<E, T>()
                .FirstOrDefault();
        }

        public List<T> DeleteDtos(List<T> dtos)
        {
            return this.Dal.BulkDelete(dtos.S2T<T, E>())
                .S2T<E, T>();
        }

        public T DeleteById(int id)
        {
            var results = this.Dal.BulkDeleteById(new BaseQueryParameters()
            {
                PrimaryKey = id,
            });
            return results.S2T<E, T>().FirstOrDefault();
        }

        public List<T> DeleteByIds(List<int> ids)
        {
            var results = this.Dal.BulkDeleteById(new BaseQueryParameters()
            {
                PrimaryKeys = ids,
            });
            return results.S2T<E, T>();
        }

        #endregion
    }
}
