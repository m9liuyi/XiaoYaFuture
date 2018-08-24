using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using XiaoYaFuture.Data.Dto;
using XiaoYaFuture.Manager.Interface;
using XiaoYaFuture.QueryParameter;

namespace XiaoYaFuture.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [Route("List")]
        public List<DTO_XYFUser> Get([FromUri]UserQueryParameters query)
        {
            return this.userManager.List(query ?? new UserQueryParameters());
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Route("GetById")]
        public DTO_XYFUser Get(int id)
        {
            return this.userManager.GetById(id);
        }

        [Route("Create")]
        public List<DTO_XYFUser> Post([FromBody]List<DTO_XYFUser> users)
        {
            return this.userManager.Create(users);
        }

        [Route("Update")]
        public DTO_XYFUser Put([FromBody]DTO_XYFUser user)
        {
            return this.userManager.Update(user);
        }

        [Route("DeleteById")]
        public DTO_XYFUser Put(int id)
        {
            return this.userManager.Delete(id);
        }

        [Route("RemoveById")]
        public DTO_XYFUser Delete(int id)
        {
            return this.userManager.Delete(id);
        }
    }
}