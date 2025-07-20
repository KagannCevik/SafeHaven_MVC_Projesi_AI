using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Change(string lang, string returnUrl)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

                HttpCookie cookie = new HttpCookie("Language");
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
            }

            return Redirect(returnUrl);
        }
    }
}