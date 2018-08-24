using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiaoYaFuture.Common;
using XiaoYaFuture.Data.Dto;
using XiaoYaFuture.QueryParameter;

namespace XiaoYaFuture.Manager.Interface
{
    public interface IUserManager : IDependency
    {
        DTO_XYFUser Create(DTO_XYFUser user);

        List<DTO_XYFUser> Create(List<DTO_XYFUser> users);

        DTO_XYFUser Update(DTO_XYFUser user);

        DTO_XYFUser Delete(int id);

        DTO_XYFUser Remove(int id);

        DTO_XYFUser Delete(DTO_XYFUser user);

        DTO_XYFUser Remove(DTO_XYFUser user);


        DTO_XYFUser GetById(int id);

        List<DTO_XYFUser> List(UserQueryParameters query);
    }
}
