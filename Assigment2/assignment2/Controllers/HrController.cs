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

    public class HrController : Controller
    {

        private readonly Traineedb16Context _db;
        public HrController(Traineedb16Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {


            return View(_db.Hrs.ToList());
        }
        public IActionResult Create()
        {
            List<Employeee> dataListLoaction = new List<Employeee>();
            dataListLoaction = _db.Employeees.ToList();
            ViewBag.dataListLoactionV = dataListLoaction;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hr obj)
        {
            _db.Hrs.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            List<Employeee> dataListLoaction = new List<Employeee>();
            dataListLoaction = _db.Employeees.ToList();
            ViewBag.dataListLoactionV = dataListLoaction;
            var x = _db.Hrs.Find(id);
            return View(x);
        }
        [HttpPost]
        public IActionResult Edit(Hr obj)
        {
            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var x = _db.Hrs.Find(id);
            _db.Hrs.Remove(x);
            _db.SaveChanges();
            return RedirectToAction("index");
        }


    }
}