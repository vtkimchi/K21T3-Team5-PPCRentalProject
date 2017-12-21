using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPCProject.Model;
using System.IO;

namespace PPCProject.Controllers
{
    public class DetailController : Controller
    {
        team35Entities model = new team35Entities();
        //
        // GET: /Detail/
        public ActionResult Detail(int id)
        {

            var detail = model.PROPERTies.FirstOrDefault(x => x.ID == id);
            ViewBag.Imagess = Directory.EnumerateFiles(Server.MapPath("~/MultiImages")).Select(fn => "~/MultiImages/" + Path.GetFileName(fn));
            ViewBag.features = model.PROPERTY_FEATURE.Where(x => x.Property_ID == id).ToList();
            return View(detail);
        }
	}
}