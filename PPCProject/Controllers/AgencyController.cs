using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPCProject.Model;

namespace PPCProject.Controllers
{
    public class AgencyController : Controller
    {
        Team35Entities model = new Team35Entities();
        //
        // GET: /Agency/
        public ActionResult Index()
        {
            var property = model.PROPERTies.ToList().OrderByDescending(x => x.ID);
            return View(property);
        }
	}
}