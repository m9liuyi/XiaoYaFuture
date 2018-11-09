using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceNet.Common;
using NiceNet.Data.Dto;
using NiceNet.Data.Entity.Context;
using NiceNet.DataAcessLayer;
using NiceNet.DataAcessLayer.Interface;
using NiceNet.Manager.Interface;
using NiceNet.QueryParameter;

namespace NiceNet.Manager
{
    public class UserManager : IUserManager
    {
        public IUserRepository UserRepository { get; set; }

        public DTO_XYFUser Create(DTO_XYFUser user)
        {
            var result = this.UserRepository.InsertDto(user);

            return result;
        }

        public List<DTO_XYFUser> Create(List<DTO_XYFUser> users)
        {
            var result = this.UserRepository.InsertDtos(users);

            return result;
        }

        public DTO_XYFUser Update(DTO_XYFUser user)
        {
            var result = this.UserRepository.UpdateDto(user);

            return result;
        }

        public List<DTO_XYFUser> Update(List<DTO_XYFUser> users)
        {
            var result = this.UserRepository.UpdateDtos(users);

            return result;
        }

        public DTO_XYFUser Delete(int id)
        {
            var result = this.UserRepository.DeleteById(id);

            return result;
        }

        public DTO_XYFUser Remove(int id)
        {
            var result = this.UserRepository.RemoveById(id);

            return result;
        }

        public DTO_XYFUser Delete(DTO_XYFUser user)
        {
            var result = this.UserRepository.DeleteDto(user);

            return result;
        }

        public DTO_XYFUser Remove(DTO_XYFUser user)
        {
            var result = this.UserRepository.RemoveDto(user);

            return result;
        }

        public DTO_XYFUser GetById(int id)
        {
            var result = this.UserRepository.GetById(id);

            return result;
        }

        public List<DTO_XYFUser> List(UserQueryParameters query)
        {
            return this.UserRepository.List(query);
        }

        public List<DTO_XYFUser> GetByIds(List<int> ids)
        {
            return this.UserRepository.GetByIds(ids);
        }

        public List<DTO_XYFUser> Delete(List<DTO_XYFUser> users)
        {
            return this.UserRepository.DeleteDtos(users);
        }

        public List<DTO_XYFUser> Delete(List<int> ids)
        {
            return this.UserRepository.DeleteByIds(ids);
        }

        public List<DTO_XYFUser> Remove(List<DTO_XYFUser> users)
        {
            return this.UserRepository.RemoveDtos(users);
        }

        public List<DTO_XYFUser> Remove(List<int> ids)
        {
            return this.UserRepository.DeleteByIds(ids);
        }
    }
}
