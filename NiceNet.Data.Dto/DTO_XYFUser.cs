﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceNet.Common;

namespace NiceNet.Data.Dto
{
    public class DTO_XYFUser : BaseDto
    {
        public int XYFUserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    
}
