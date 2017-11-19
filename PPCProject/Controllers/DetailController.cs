using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPCProject.Model;

namespace PPCProject.Controllers
{
    public class DetailController : Controller
    {
        Team35Entities model = new Team35Entities();
        //
        // GET: /Detail/
        public ActionResult Detail(int id)
        {
            var detail = model.PROPERTies.FirstOrDefault(x => x.ID == id);
            return View(detail);
        }
	}
}