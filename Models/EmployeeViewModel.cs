using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet4.Models
{
    public class EmployeeViewModel
    { public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    public string Email { get; set; }
    public int DepartmentId { get; set; }
    public List<HobbyViewModel> Hobbies { get; set; }
    public string Gender { get; set; }

     public IFormFile ImageFile { get; set; }
    public string ImagePath { get; set; }


    }
}