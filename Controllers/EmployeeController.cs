using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using projet4.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace projet4.Controllers
{
    [Route("[controller]")]
    [Route("[controller]/[Action]")]
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        string cs = "";
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            cs = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }


        public IActionResult Create()
        {
            EmployeeViewModel model = new EmployeeViewModel
            {
                // Other properties initialization

                Hobbies = GetHobbies()
            };

            // Additional code

            return View(model);

            // return View();
        }

        private List<HobbyViewModel> GetHobbies()
        {
            // Logic to fetch and return a list of hobbies
            // You can fetch hobbies from a database, hardcode them, or use any other data source

            List<HobbyViewModel> hobbies = new List<HobbyViewModel>
         {
        new HobbyViewModel { Name = "Hobby 1", IsSelected = false },
        new HobbyViewModel { Name = "Hobby 2", IsSelected = false },
        // Add more hobbies as needed
         };

            return hobbies;
        }

        [HttpPost]

        public IActionResult Create(EmployeeViewModel model, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                string imagePath = Path.Combine(directoryPath, fileName);
                Directory.CreateDirectory(directoryPath); // Create the directory if it doesn't exist
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                model.ImagePath = "/Images/" + fileName;
            }


            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("InsertEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Set stored procedure parameters
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@Salary", model.Salary);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                string hobbyIds = string.Join(",", model.Hobbies.Where(h => h.IsSelected).Select(h => h.HobbyId));
                command.Parameters.AddWithValue("@Hobbies", hobbyIds);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                command.Parameters.AddWithValue("@ImagePath", model.ImagePath);

                // Execute the stored procedure
                command.ExecuteNonQuery();
            }

            // Redirect to a success page or perform other actions
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ActionName()
        {

            return View();
        }
    }


}
