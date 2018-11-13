using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceNet.Common;
using NiceNet.QueryParameter;

namespace NiceNet.DataAcessLayer.Interface
{
    public interface IBaseRepository<T, E> 
        where T : BaseDto
        where E : BaseEntity
    {
        #region search

        T GetById(int id);

        List<T> GetByIds(List<int> ids);

        List<T> List<M>(M query) where M : BaseQueryParameters;

        #endregion

        #region insert

        T InsertDto(T dto);

        List<T> InsertDtos(List<T> dtos);

        #endregion

        #region update

        T UpdateDto(T dto);

        List<T> UpdateDtos(List<T> dtos);

        /// <summary>
        /// 编辑ids中的指定几个字段
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="columnValues"></param>
        /// <returns></returns>
        List<T> UpdateProperties(List<int> ids, Dictionary<string, object> columnValues);

        #endregion

        #region remove

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        T RemoveDto(T dto);

        /// <summary>
        /// 物理删除，所有Entity与Dto中必须用IsDelete字段标识删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T RemoveById(int id);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        List<T> RemoveDtos(List<T> dtos);

        /// <summary>
        /// 物理删除，所有Entity与Dto中必须用IsDelete字段标识删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<T> RemoveByIds(List<int> ids);

        #endregion

        #region delete

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        T DeleteDto(T dto);

        /// <summary>
        /// 逻辑删除，所有Entity与Dto中必须用IsDelete字段标识删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        T DeleteById(int id);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        List<T> DeleteDtos(List<T> dtos);

        /// <summary>
        /// 逻辑删除，所有Entity与Dto中必须用IsDelete字段标识删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<T> DeleteByIds(List<int> ids);

        #endregion
    }
}
