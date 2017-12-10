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
    public class AgencyController : Controller
    {
        public static string idd ="";
        Team35Entities model = new Team35Entities();
        //
        // GET: /Agency/
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                if (int.Parse(Session["RoleID"].ToString()) == 1)
                {
                    idd = Session["UserID"].ToString();
                    int user_id = int.Parse(idd);
                    var property = model.PROPERTies.OrderByDescending(x => x.ID).Where(x => x.UserID == user_id).ToList();
                    return View(property);
                }
                else
                {
                    return RedirectToAction("Index", "Admin/ProductAdmin");
                }
                //idd = Session["UserID"].ToString();
                //var property = model.PROPERTies.OrderByDescending(x => x.ID).Where(x => x.UserID == user_id).ToList();
                //return View(property);
            }
            else
            {
                return RedirectToAction("Login", "Agency");
            }
        }
        

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = model.USERs.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                if (user.Password.Equals(password))
                {
                    Session["FullName"] = user.FullName;
                    Session["UserID"] = user.ID;
                    Session["RoleID"] = user.Role;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.mess = "* Account Invalid";
            }
            return View();
        }
        public ActionResult Logout()
        {
            var user = model.USERs;
            if (user != null)
            {
                Session["FullName"] = null;
                Session["UserID"] = null;
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Create()
        {
            ListItem();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PROPERTY pROPERTY, List<HttpPostedFileBase> files)
        {
            ListItem();
            var product = new PROPERTY();

            try
            {

                string filename = Path.GetFileNameWithoutExtension(pROPERTY.AvatarFile.FileName);
                string extension = Path.GetExtension(pROPERTY.AvatarFile.FileName);
                filename = filename + "checkcheck" + DateTime.Now.ToString("yymmssfff") + extension;
                pROPERTY.Avatar = filename;
                filename = Path.Combine(Server.MapPath("~/Images"), filename);
                // Avatar

                if (Path.GetFileNameWithoutExtension(pROPERTY.AvatarFile.FileName) == null)
                {
                    string s2 = "~/Images/ImagesNull.png";
                    pROPERTY.AvatarFile.SaveAs(s2);
                    //property.ImageFile2.SaveAs(filename2);
                }
                else
                {
                    //property.ImageFile2.SaveAs(filename2);
                    pROPERTY.AvatarFile.SaveAs(filename);
                }

                pROPERTY.Created_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                pROPERTY.UserID = int.Parse(idd);
                pROPERTY.Status_ID = 1;
                var model = new XulyModels();
                if (ModelState.IsValid)
                {
                    long id = model.Insert(pROPERTY);
                    var path = "";
                    foreach (var item in files)
                    {
                        if (item != null)
                        {
                            if (item.ContentLength > 0)
                            {
                                if (Path.GetExtension(item.FileName).ToLower() == ".jpg"
                                    || Path.GetExtension(item.FileName).ToLower() == ".png"
                                    || Path.GetExtension(item.FileName).ToLower() == ".gif"
                                    || Path.GetExtension(item.FileName).ToLower() == ".jpeg")
                                {
                                    var path0 = id + item.FileName;
                                    path = Path.Combine(Server.MapPath("~/MultiImages"), path0);

                                    item.SaveAs(path);
                                    ViewBag.UploadSuccess = true;

                                }
                            }
                        }
                    }
                    if (id > 0)

                    {
                        return RedirectToAction("Index", "Agency", new { @user_id = Session["UserID"] });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Create khong thanh cong");
                    }
                }


            }
            catch(NullReferenceException)
            {
                pROPERTY.Created_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                pROPERTY.UserID = int.Parse(idd);
                pROPERTY.Status_ID = 1;
                var model = new XulyModels();
                if (ModelState.IsValid)
                {
                    long id = model.Insert(pROPERTY);
                    var path = "";
                    foreach (var item in files)
                    {
                        if (item != null)
                        {
                            if (item.ContentLength > 0)
                            {
                                if (Path.GetExtension(item.FileName).ToLower() == ".jpg"
                                    || Path.GetExtension(item.FileName).ToLower() == ".png"
                                    || Path.GetExtension(item.FileName).ToLower() == ".gif"
                                    || Path.GetExtension(item.FileName).ToLower() == ".jpeg")
                                {
                                    var path0 = id + item.FileName;
                                    path = Path.Combine(Server.MapPath("~/MultiImages"), path0);

                                    item.SaveAs(path);
                                    ViewBag.UploadSuccess = true;

                                }
                            }
                        }
                    }
                    if (id > 0)

                    {
                        return RedirectToAction("Index", "Agency", new { @user_id = Session["UserID"] });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Create khong thanh cong");
                    }
                }
            }

            return View();
        }


        public void ListItem()
        {
            ViewBag.property_type = model.PROPERTY_TYPE.ToList();
            ViewBag.ward = model.WARDs.OrderByDescending(x => x.ID).ToList();
            ViewBag.street = model.STREETs.OrderByDescending(x => x.ID).ToList();
            ViewBag.district = model.DISTRICTs.OrderByDescending(x => x.ID).ToList();
            ViewBag.user = model.USERs.OrderByDescending(x => x.ID).ToList();
            ViewBag.status = model.PROJECT_STATUS.OrderByDescending(x => x.ID).ToList();

        }

        public JsonResult GetStreet(int Distric_id)
        {
            return Json(model.STREETs.Where(x => x.District_ID == Distric_id)
                .Select(x => new { id = x.ID, text = x.StreetName }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWard(int Distric_id)
        {
            return Json(model.WARDs.Where(x => x.District_ID == Distric_id)
                .Select(x => new { id = x.ID, text = x.WardName }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}