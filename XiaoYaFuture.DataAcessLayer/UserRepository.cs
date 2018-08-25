using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XiaoYaFuture.Data.Dto;
using XiaoYaFuture.Data.Entity.Context;
using XiaoYaFuture.DataAcessLayer.Interface;

namespace XiaoYaFuture.DataAcessLayer
{
    public class UserRepository : BaseRepository<DTO_XYFUser, XYFUser>, IUserRepository
    {
        public UserRepository(IBaseDal<XYFUser> dal) : base(dal)
        {

        }
    }
}
