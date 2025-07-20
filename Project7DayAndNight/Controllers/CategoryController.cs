using Project7DayAndNight.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class CategoryController : Controller
    {
        DayNightDbEntities db = new DayNightDbEntities();
        public ActionResult Categorylist()
        {
            var values = db.TblCategory.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(TblCategory category)
        {
            db.TblCategory.Add(category);
            db.SaveChanges();
            return View();
        }
    }
}