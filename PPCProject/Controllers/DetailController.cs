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
        DemoPPCRentalEntities2 model = new DemoPPCRentalEntities2();
        //
        // GET: /Detail/
        public ActionResult Detail(int id)
        {
            var detail = model.PROPERTies.FirstOrDefault(x => x.ID == id);
            return View(detail);
        }
	}
}