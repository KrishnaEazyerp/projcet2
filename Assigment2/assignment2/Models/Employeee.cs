using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace assignment2.Models
{
    public partial class Employeee
    {
        public Employeee()
        {
            Hrs = new HashSet<Hr>();
        }

        public int EmpId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public int? ContactNo { get; set; }
        public string? HomeAddress { get; set; }
        public string? WorkLocation { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Hr> Hrs { get; set; }
    }
}
