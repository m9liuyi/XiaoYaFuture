using NiceNet.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceNet.Data.Entity
{
    public class XYFDepartment : BaseEntity
    {
        [Key]
        public int XYFDepartmentId { get; set; }

        public string Name { get; set; }
    }
}
