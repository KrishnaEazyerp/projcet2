using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace assignment2
{
    public class EmployeeeController : Controller
    {
        private readonly Traineedb16Context _db;
        public EmployeeeController(Traineedb16Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            ViewBag.data2 = _db.Employeees.Where(x => x.IsActive == false).ToList();
            return View(_db.Employeees.ToList());
        }
        public IActionResult Create()
        {

            List<Location> dplist = new List<Location>();
            dplist = _db.Locations.ToList();
            ViewBag.list = dplist;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employeee obj)
        {
            if (ModelState.IsValid)
            {
                _db.Employeees.Add(obj);
                _db.SaveChanges();
                TempData["msg"] = "<script>alert('Registration succesfully')</script>";
                return RedirectToAction("index");
            }
            List<Location> dplist = new List<Location>();
            dplist = _db.Locations.ToList();
            ViewBag.list = dplist;
            return View();
        }
        public IActionResult Edit(int id)
        {
            List<Location> dplist = new List<Location>();
            dplist = _db.Locations.ToList();
            ViewBag.list = dplist;
            var x = _db.Employeees.Find(id);
            return View(x);
        }
        [HttpPost]
        public IActionResult Edit(Employeee obj)
        {
            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            TempData["msg"] = "<script>alert('update succesfully')</script>";
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var x = _db.Employeees.Find(id);
            _db.Employeees.Remove(x);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult SoftDelete(int id)
        {
            var e = _db.Employeees.Find(id);
            if (e != null)
            {
                e.IsActive = false;
                _db.SaveChanges();
            }
            return RedirectToAction("index");
        }
        public IActionResult SelectEmp(int id)
        {
            var emp = _db.Employeees.Find(id);
            if (emp != null)
            {
                emp.IsDelete = true;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}