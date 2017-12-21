using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPCProject.Model;
using System.IO;
using PPCProject.Code;

namespace PPCProject.Controllers
{
    public class UserController : Controller
    {
        team35Entities model = new team35Entities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            int id = (int)Session["UserID"];
            var userdetail = model.USERs.FirstOrDefault(x => x.ID == id);
            return View(userdetail);
        }
        [HttpPost]
        public ActionResult ChangePassword(string currentpassword, string newpassword, string confirmnewpassword)
        {
            int id = (int)Session["UserID"];
            var userdetail = model.USERs.FirstOrDefault(x => x.ID == id);
            if (currentpassword == userdetail.Password)
            {
                if (confirmnewpassword == newpassword)
                {
                    userdetail.Password = newpassword;
                    model.SaveChanges();
                    TempData["changepassword"] = "Your password has been changed";
                    return RedirectToAction("Login", "Agency");
                }
                else
                {
                    ModelState.AddModelError("confirmerror", "ConfirmPassword is wrong");
                    return View(userdetail);
                }
            }
            else
            {
                ModelState.AddModelError("passworderror", "Password is wrong");
                return View(userdetail);
            }
            
        }
    }
}