using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPCProject.Model;

namespace PPCProject.Controllers
{
    public class AboutUsController : Controller
    {
        team35Entities model = new team35Entities();
        // GET: AboutUs
        public ActionResult AboutUs()
        {
            var AboutUs = model.ABOUT_US.FirstOrDefault(x => x.ID == 31);

            return View(AboutUs);
        }
    }
}