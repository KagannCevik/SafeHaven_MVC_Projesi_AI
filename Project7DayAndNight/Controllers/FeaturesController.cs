using Project7DayAndNight.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class FeaturesController : Controller
    {
        // GET: Features
        DayNightDbEntities db = new DayNightDbEntities();
        public ActionResult Index()
        {
            var values = db.TblFeatures.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TblFeatures tblFeatures)
        {
            if (ModelState.IsValid)
            {
                db.TblFeatures.Add(tblFeatures);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblFeatures);
        }
        public ActionResult Delete(int id)
        {
            var feature = db.TblFeatures.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            db.TblFeatures.Remove(feature);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var feature = db.TblFeatures.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }
        [HttpPost]
        public ActionResult Update(TblFeatures tblFeatures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFeatures).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblFeatures);
        }
    }
}