﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NiceNet.Data.Dto;
using NiceNet.Data.Entity.Context;
using NiceNet.DataAcessLayer.Interface;

namespace NiceNet.DataAcessLayer
{
    public class UserRepository : BaseRepository<DTO_XYFUser, XYFUser>, IUserRepository
    {
        public UserRepository(IBaseDal<XYFUser> dal) : base(dal)
        {

        }
    }
}