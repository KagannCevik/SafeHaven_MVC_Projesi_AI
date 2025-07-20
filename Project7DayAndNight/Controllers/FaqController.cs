using Project7DayAndNight.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class FaqController : Controller
    {
        // GET: Faq
        DayNightDbEntities db = new DayNightDbEntities();
        public ActionResult Index()
        {
            var faqList = db.TblFaq.ToList();
            return View(faqList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TblFaq faq)
        {
            if (ModelState.IsValid)
            {
                db.TblFaq.Add(faq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faq);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var faq = db.TblFaq.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }
        [HttpPost]
        public ActionResult Edit(TblFaq faq)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faq).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faq);
        }
       public ActionResult Delete(int id)
        {
            var faq = db.TblFaq.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            db.TblFaq.Remove(faq);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}