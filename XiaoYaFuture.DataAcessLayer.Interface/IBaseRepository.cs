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
        #region search

        T GetById(int id);

        List<T> GetByIds(List<int> ids);

        List<T> List<M>(M query) where M : BaseQueryParameters;

        #endregion

        #region insert

        T Insert(T dto);

        List<T> Insert(List<T> dtos);

        #endregion

        #region update

        T Update(T dto);

        List<T> Update(List<T> dtos);

        /// <summary>
        /// 编辑ids中的指定几个字段
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="columnValues"></param>
        /// <returns></returns>
        List<T> Update(List<int> ids, Dictionary<string, object> columnValues);

        #endregion

        #region remove

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        T Remove(T dto);

        /// <summary>
        /// 物理删除，所有Entity与Dto中必须用IsDelete字段标识删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Remove(int id);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        List<T> Remove(List<T> dtos);

        /// <summary>
        /// 物理删除，所有Entity与Dto中必须用IsDelete字段标识删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<T> Remove(List<int> ids);

        #endregion

        #region delete

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        T Delete(T dto);

        /// <summary>
        /// 逻辑删除，所有Entity与Dto中必须用IsDelete字段标识删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        T Delete(int id);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        List<T> Delete(List<T> dtos);

        /// <summary>
        /// 逻辑删除，所有Entity与Dto中必须用IsDelete字段标识删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<T> Delete(List<int> ids);

        #endregion
    }
}
