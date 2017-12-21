using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPCProject.Model;

namespace PPCProject.Controllers
{
    public class MyProfileController : Controller
    {
        //
        // GET: /MyProfile/
        team35Entities tt = new team35Entities();
        public ActionResult Index()
        {
            //var user = tt.USERs.Find(user_id);
            var profile = tt.USERs.OrderByDescending(x => x.ID).ToList().Where(x => x.ID == int.Parse(Session["UserID"].ToString()));
            return View(profile);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = tt.USERs.FirstOrDefault(x => x.ID == id);
            //ViewBag.email = tt.USERs.OrderByDescending(x => x.ID).ToList();
            ViewBag.fullname = tt.USERs.OrderByDescending(x => x.ID).ToList();
            ViewBag.phone = tt.USERs.OrderByDescending(x => x.ID).ToList();
            ViewBag.address = tt.USERs.OrderByDescending(x => x.ID).ToList();
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(int id, USER u)
        {
            var user = tt.USERs.FirstOrDefault(x => x.ID == id);
            //user.Email = u.Email;
            user.FullName = u.FullName;
            user.Phone = u.Phone;
            user.Address = u.Address;
            tt.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}