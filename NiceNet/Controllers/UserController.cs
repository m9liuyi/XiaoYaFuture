using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NiceNet.Data.Dto;
using NiceNet.Manager.Interface;
using NiceNet.QueryParameter;

namespace NiceNet.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// 根据 query 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("List")]
        public List<DTO_XYFUser> Get([FromUri]UserQueryParameters query)
        {
            return this.userManager.List(query ?? new UserQueryParameters());
        }

        /// <summary>
        /// 根据 Id 查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Route("GetById")]
        public DTO_XYFUser Get(int id)
        {
            return this.userManager.GetById(id);
        }

        /// <summary>
        /// 根据 Ids 查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Route("GetByIds")]
        public List<DTO_XYFUser> Get([FromUri]List<int> ids)
        {
            return this.userManager.GetByIds(ids);
        }

        [Route("Create")]
        public List<DTO_XYFUser> Post([FromBody]List<DTO_XYFUser> users)
        {
            return this.userManager.Create(users);
        }

        [Route("Update")]
        public List<DTO_XYFUser> Put([FromBody]List<DTO_XYFUser> users)
        {
            return this.userManager.Update(users);
        }

        [Route("DeleteById")]
        public DTO_XYFUser Put(int id)
        {
            return this.userManager.Delete(id);
        }

        [Route("DeleteByIds")]
        public List<DTO_XYFUser> Put(List<int> ids)
        {
            return this.userManager.Delete(ids);
        }

        [Route("RemoveById")]
        public DTO_XYFUser Delete(int id)
        {
            return this.userManager.Remove(id);
        }

        [Route("RemoveByIds")]
        public List<DTO_XYFUser> Delete(List<int> ids)
        {
            return this.userManager.Remove(ids);
        }
    }
}