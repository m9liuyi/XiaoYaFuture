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
        DTO_XYFUser GetById(int id);
        List<DTO_XYFUser> GetByIds(List<int> ids);
        List<DTO_XYFUser> List(UserQueryParameters query);

        DTO_XYFUser Create(DTO_XYFUser user);
        List<DTO_XYFUser> Create(List<DTO_XYFUser> users);

        DTO_XYFUser Update(DTO_XYFUser user);
        List<DTO_XYFUser> Update(List<DTO_XYFUser> users);
        
        DTO_XYFUser Delete(DTO_XYFUser user);
        List<DTO_XYFUser> Delete(List<DTO_XYFUser> users);
        DTO_XYFUser Delete(int id);
        List<DTO_XYFUser> Delete(List<int> ids);
        
        DTO_XYFUser Remove(DTO_XYFUser user);
        List<DTO_XYFUser> Remove(List<DTO_XYFUser> users);
        DTO_XYFUser Remove(int id);
        List<DTO_XYFUser> Remove(List<int> ids);
    }
}
