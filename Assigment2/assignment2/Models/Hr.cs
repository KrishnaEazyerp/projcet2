using System;
using System.Collections.Generic;

namespace assignment2.Models
{
    public partial class Hr
    {
        public int HrId { get; set; }
        public string? EmpPayrollInfo { get; set; }
        public int? SocialSecurityNo { get; set; }
        public int? Salary { get; set; }
        public int? EmpId { get; set; }

        public virtual Employeee? Emp { get; set; }
    }
}
