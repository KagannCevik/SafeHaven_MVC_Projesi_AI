using Project7DayAndNight.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        DayNightDbEntities db = new DayNightDbEntities();
        public ActionResult Index()
        {
            var sliderList = db.TblSlider.ToList();
            return View(sliderList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TblSlider slider)
        {
            // Dosya input'unun adı ile Request'ten dosyayı al
            var file = Request.Files["SliderImageFile"];
            if (file != null && file.ContentLength > 0)
            {
                // Dosya adını ve yolu oluştur
                var fileName = Path.GetFileName(file.FileName);
                var folderPath = Server.MapPath("~/img/");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);
                file.SaveAs(filePath);

                // Veritabanına kaydedilecek yol (önemli!)
                slider.SliderImage = "/img/" + fileName;
            }
            else
            {
                ViewBag.ImageError = "Lütfen bir görsel seçiniz.";
                return View(slider);
            }

            // Diğer alanlar için validasyonunu yap ve kaydet
            if (ModelState.IsValid)
            {
                db.TblSlider.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var slider = db.TblSlider.Find(id);
            if (slider != null)
            {
                db.TblSlider.Remove(slider);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var slider = db.TblSlider.Find(id);
            if (slider == null)
                return HttpNotFound();
            return View(slider);
        }

        // POST: Slider/Edit/5
        [HttpPost]
      
        public ActionResult Edit(TblSlider slider)
        {
            var file = Request.Files["SliderImageFile"];
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var folderPath = Server.MapPath("~/LifeSure-1.0.0/img/");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);
                file.SaveAs(filePath);

                slider.SliderImage = "/LifeSure-1.0.0/img/" + fileName;
            }
            // Eğer yeni resim yüklenmezse eski resim kalır

            if (ModelState.IsValid)
            {
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

    }
}