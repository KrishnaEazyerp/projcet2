using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace assignment2.Controllers
{

    public class LocationController : Controller
    {
        private readonly Traineedb16Context _db;
        public LocationController(Traineedb16Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {


            return View(_db.Locations.ToList());
        }
        public IActionResult Create()
        {
            List<Employeee> dataListLoaction = new List<Employeee>();
            dataListLoaction = _db.Employeees.ToList();
            ViewBag.dataListLoactionV = dataListLoaction;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Location obj)
        {
            _db.Locations.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            List<Employeee> dataListLoaction = new List<Employeee>();
            dataListLoaction = _db.Employeees.ToList();
            ViewBag.dataListLoactionV = dataListLoaction;
            var x = _db.Locations.Find(id);
            return View(x);
        }
        [HttpPost]
        public IActionResult Edit(Location obj)
        {
            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var x = _db.Locations.Find(id);
            _db.Locations.Remove(x);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}