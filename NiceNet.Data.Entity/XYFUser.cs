using NiceNet.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceNet.Data.Entity
{
    public class XYFUser : BaseEntity
    {
        [Key]
        public int XYFUserId { get; set; }

        public string Name { get; set; }
    }
}
