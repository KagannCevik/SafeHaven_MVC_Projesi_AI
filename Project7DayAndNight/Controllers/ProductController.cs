using Project7DayAndNight.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DayNightDbEntities db = new DayNightDbEntities();
        public ActionResult ProductList()
        {
            var values = db.TblProduct.ToList();
            return View(values);
        }
        public ActionResult ProductListWithCategory()
        {
            var values =db.TblProduct.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(TblProduct t)
        {
            t.ProductStatus = false;
            db.TblProduct.Add(t);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
        public ActionResult DeleteProduct(int id)
        {
            var value=db.TblProduct.Find(id);
            db.TblProduct.Remove(value);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            var value = db.TblProduct.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateProduct(TblProduct t)
        {
            var value = db.TblProduct.Find(t.ProductId);
            value.ProductName = t.ProductName;
            value.ProductStok = t.ProductStok;
            value.ProductStatus = t.ProductStatus;
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}