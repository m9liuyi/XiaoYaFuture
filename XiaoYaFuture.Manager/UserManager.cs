using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiaoYaFuture.Common;
using XiaoYaFuture.Data.Dto;
using XiaoYaFuture.Data.Entity.Context;
using XiaoYaFuture.DataAcessLayer;
using XiaoYaFuture.DataAcessLayer.Interface;
using XiaoYaFuture.Manager.Interface;
using XiaoYaFuture.QueryParameter;

namespace XiaoYaFuture.Manager
{
    public class UserManager : IUserManager
    {
        private UserRepository userRepository { get; set; }

        public UserManager(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public DTO_XYFUser Create(DTO_XYFUser user)
        {
            var result = this.userRepository.Insert(user);

            return result;
        }

        public List<DTO_XYFUser> Create(List<DTO_XYFUser> users)
        {
            var result = this.userRepository.Insert(users);

            return result;
        }

        public DTO_XYFUser Update(DTO_XYFUser user)
        {
            var result = this.userRepository.Update(user);

            return result;
        }

        public List<DTO_XYFUser> Update(List<DTO_XYFUser> users)
        {
            var result = this.userRepository.Update(users);

            return result;
        }

        public DTO_XYFUser Delete(int id)
        {
            var result = this.userRepository.Delete(id);

            return result;
        }

        public DTO_XYFUser Remove(int id)
        {
            var result = this.userRepository.Remove(id);

            return result;
        }

        public DTO_XYFUser Delete(DTO_XYFUser user)
        {
            var result = this.userRepository.Delete(user);

            return result;
        }

        public DTO_XYFUser Remove(DTO_XYFUser user)
        {
            var result = this.userRepository.Remove(user);

            return result;
        }

        public DTO_XYFUser GetById(int id)
        {
            var result = this.userRepository.GetById(id);

            return result;
        }

        public List<DTO_XYFUser> List(UserQueryParameters query)
        {
            return this.userRepository.List(query);
        }

        public List<DTO_XYFUser> GetByIds(List<int> ids)
        {
            return this.userRepository.GetByIds(ids);
        }

        public List<DTO_XYFUser> Delete(List<DTO_XYFUser> users)
        {
            return this.userRepository.Delete(users);
        }

        public List<DTO_XYFUser> Delete(List<int> ids)
        {
            return this.userRepository.Delete(ids);
        }

        public List<DTO_XYFUser> Remove(List<DTO_XYFUser> users)
        {
            return this.userRepository.Remove(users);
        }

        public List<DTO_XYFUser> Remove(List<int> ids)
        {
            return this.userRepository.Delete(ids);
        }
    }
}
