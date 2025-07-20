using Project7DayAndNight.Models;
using Project7DayAndNight.Models.DataModels;
using Project7DayAndNight.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DayNightDbEntities db = new DayNightDbEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexEN()
        {
            return View();
        }
        public PartialViewResult IndexEnHead()
        {
            var sliderEn = db.TblSliderEn.FirstOrDefault();
            return PartialView(sliderEn);
        }
        public PartialViewResult HeadPartial()
        {
            
            return PartialView();
        }
        public PartialViewResult HeaderPartial()
        {
            var linkedinService = new LinkedinService();
            int followers = 90278;
            try
            {
                var task = linkedinService.GetUserInfo("muratyucedag");
                if (task.Wait(3000)) // 3 saniye timeout
                {
                    var model = task.Result;
                    followers = model.FollowersCount;
                }
            }
            catch
            {
                followers = 0; // veya hata mesajı
            }

            ViewBag.FollowersCount = followers;
            return PartialView("HeaderPartial");
        }

        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult SearchPartial()
        {
            return PartialView();
        }
        public PartialViewResult SliderPartial()
        {
            var values = db.TblSlider.ToList();
            return PartialView(values);
        }
        public PartialViewResult FeaturePartial()
        {
            var values = db.TblFeatures.ToList();
            return PartialView(values);
        }
        public PartialViewResult AboutPartial()
        {
            var values = db.TblAbout.ToList();
            return PartialView(values);
        }
        public PartialViewResult ServicePartial()
        {
            var values = db.TblServices.ToList();
            return PartialView(values);
        }
        public PartialViewResult FaqPartial()
        {
            var values = db.TblFaq.ToList();
            return PartialView(values);
        }
        public PartialViewResult TeamPartial()
        {
            var values = db.TblTeam.ToList();
            return PartialView(values);
        }
        public PartialViewResult TestimonialPartial()
        {
            var testimonials = db.TblTestimonial.ToList();
            return PartialView(testimonials);
        }
        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        public PartialViewResult DesignedPartial()
        {
            return PartialView();
        }
        public PartialViewResult ScriptsPartial()
        {
            return PartialView();
        }
    }
}