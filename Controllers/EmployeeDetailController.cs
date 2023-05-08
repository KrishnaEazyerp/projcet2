using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using projet4.Models;
using System.Data;
using PagedList;

namespace projet4.Controllers
{
    [Route("[controller]")]
    [Route("[controller]/[Action]")]
    public class EmployeeDetailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        string cs = "";
        public EmployeeDetailController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            cs = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }

        public IActionResult index()
        {

            return View();
            //return RedirectToAction("RetrieveAllEmployees");
        }
        // [HttpGet]
        // public IActionResult RetrieveAllEmployees()
        // {
        //     List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

        //     using (SqlConnection connection = new SqlConnection(cs))
        //     {
        //         connection.Open();
        //         string sqldata = "select * from EmployeeTable";
        //         SqlCommand command = new SqlCommand(sqldata, connection);
        //         // command.CommandType = CommandType.StoredProcedure;

        //         using (SqlDataReader reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 EmployeeViewModel employee = new EmployeeViewModel
        //                 {
        //                     EmployeeId = (int)reader["EmployeeId"],
        //                     FirstName = (string)reader["FirstName"],
        //                     LastName = (string)reader["LastName"],
        //                     Salary = (decimal)reader["Salary"],
        //                     Email = (string)reader["Email"],
        //                     DepartmentId = (int)reader["DepartmentId"],
        //                     Gender = (string)reader["Gender"],
        //                     ImagePath = (string)reader["ImagePath"]
        //                 };

        //                 employees.Add(employee);
        //             }
        //         }
        //     }

        //     return View(employees);
        // }

 

// [HttpGet]
// public IActionResult RetrieveAllEmployees(int? page)
// {
//     const int pageSize = 10; // Set the number of items per page

//     List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

//     using (SqlConnection connection = new SqlConnection(cs))
//     {
//         connection.Open();
//         string sqldata = "select * from EmployeeTable";
//         SqlCommand command = new SqlCommand(sqldata, connection);

//         using (SqlDataReader reader = command.ExecuteReader())
//         {
//             while (reader.Read())
//             {
//                 EmployeeViewModel employee = new EmployeeViewModel
//                 {
//                     EmployeeId = (int)reader["EmployeeId"],
//                     FirstName = (string)reader["FirstName"],
//                     LastName = (string)reader["LastName"],
//                     Salary = (decimal)reader["Salary"],
//                     Email = (string)reader["Email"],
//                     DepartmentId = (int)reader["DepartmentId"],
//                     Gender = (string)reader["Gender"],
//                     ImagePath = (string)reader["ImagePath"]
//                 };

//                 employees.Add(employee);
//             }
//         }
//     }

//     // Convert the list of employees to a PagedList
//     var pagedEmployees = employees.ToPagedList(page ?? 1, pageSize);

//     return View(pagedEmployees);
// }

[HttpGet]
public IActionResult RetrieveAllEmployees(int? page)
{
    int pageSize = 10;
    int pageNumber = (page ?? 1);

    IPagedList<EmployeeViewModel> employees = null;

    using (SqlConnection connection = new SqlConnection(cs))
    {
        connection.Open();
        string sqldata = "SELECT * FROM EmployeeTable";
        SqlCommand command = new SqlCommand(sqldata, connection);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();

            while (reader.Read())
            {
                EmployeeViewModel employee = new EmployeeViewModel
                {
                    EmployeeId = (int)reader["EmployeeId"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Salary = (decimal)reader["Salary"],
                    Email = (string)reader["Email"],
                    DepartmentId = (int)reader["DepartmentId"],
                    Gender = (string)reader["Gender"],
                    ImagePath = (string)reader["ImagePath"]
                };

                employeeList.Add(employee);
            }

            employees = employeeList.ToPagedList(pageNumber, pageSize);
        }
    }

    return View(employees);
}



        public IActionResult RetrieveById()
        {

            return View();
        }

        [HttpGet]
        public IActionResult RetrieveById(EmployeeViewModel e)
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            EmployeeViewModel employee = null;
            if (e.EmployeeId == 0)
            {
                return View();
            }

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                string sql = "select * from  EmployeeTable";
                SqlCommand command = new SqlCommand(sql, connection);
                //command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", e.EmployeeId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new EmployeeViewModel
                        {
                            EmployeeId = (int)reader["EmployeeId"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Salary = (decimal)reader["Salary"],
                            Email = (string)reader["Email"],
                            DepartmentId = (int)reader["DepartmentId"],
                            Gender = (string)reader["Gender"],
                            ImagePath = (string)reader["ImagePath"]
                        };
                        employees.Add(employee);
                    }
                }

            }
            return View(employees);
        }

        public IActionResult Delete(EmployeeViewModel e)
        {
            using(SqlConnection con= new SqlConnection(cs))
            {
                if(e.EmployeeId==0)
                {
                    return View();

                }
                string sql=$"delete from EmployeeTable where EmployeeId='{e.EmployeeId}'";
                SqlCommand cmd=new SqlCommand(sql,con);
                con.Open();
                cmd.ExecuteNonQuery();
                
                
            }
            
            return RedirectToAction("Index","Home");
        }

    }
}