using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NiceNet.Common;
using NiceNet.Data.Dto;
using NiceNet.Data.Entity.Context;

namespace NiceNet.DataAcessLayer.Interface
{
    public interface IUserRepository : IBaseRepository<DTO_XYFUser, XYFUser>
    {

    }
}
