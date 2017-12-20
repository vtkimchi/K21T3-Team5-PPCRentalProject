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
        public static string idd = "";
        team35Entities modeldb = new team35Entities();
        //
        // GET: /Agency/
        public ActionResult Index()
        {

            if ((Session["UserID"] != null) && (int.Parse(Session["RoleID"].ToString()) == 1))
            {

                idd = Session["UserID"].ToString();
                int user_id = int.Parse(idd);
                var property = modeldb.PROPERTies.OrderByDescending(x => x.ID).Where(x => x.UserID == user_id).ToList();
                return View(property);

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
            var user = modeldb.USERs.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                if (user.Password.Equals(password))
                {
                    Session["FullName"] = user.FullName;
                    Session["UserID"] = user.ID;
                    Session["RoleID"] = user.Role;

                    if (int.Parse(Session["RoleID"].ToString()) == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index", "ProductAdmin", new { area = "Admin" });
                    }
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
            var user = modeldb.USERs;
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
        public ActionResult Create(PROPERTY pROPERTY, List<HttpPostedFileBase> files, string submit)
        {
            ListItem();
            var product = new PROPERTY();

            try
            {
                // xu ly Image
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
                pROPERTY.UnitPrice = "VND";
                //set status post project
                if (submit == "Post")
                {
                    pROPERTY.Status_ID = 1;
                }
                else if (submit == "Draf")
                {
                    pROPERTY.Status_ID = 2;
                }



                var model = new XulyModels();
                if (ModelState.IsValid)
                {

                    long id = model.Insert(pROPERTY);
                    
                    
                    //save feature
                    PROPERTY_FEATURE pf = new PROPERTY_FEATURE();

                    foreach (string x in pROPERTY.listfeature)
                    {
                        pf.Property_ID = (int)id;
                        pf.Feature_ID = int.Parse(x);
                        modeldb.PROPERTY_FEATURE.Add(pf);
                        modeldb.SaveChanges();
               
                    }

                    //save mutiImage
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

                    // end save nhieu ảnh
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
            catch (NullReferenceException)
            {
                pROPERTY.Created_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                pROPERTY.UserID = int.Parse(idd);

                //set status post project
                if (submit == "Post")
                {
                    pROPERTY.Status_ID = 1;
                }
                else if (submit == "Draf")
                {
                    pROPERTY.Status_ID = 2;
                }


                var model = new XulyModels();
                if (ModelState.IsValid)
                {
                    long id = model.Insert(pROPERTY);

                    PROPERTY_FEATURE pf = new PROPERTY_FEATURE();

                    foreach (string x in pROPERTY.listfeature)
                    {
                        pf.Property_ID = (int)id;
                        pf.Feature_ID = int.Parse(x);
                        modeldb.PROPERTY_FEATURE.Add(pf);
                        modeldb.SaveChanges();

                    }

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
        //hàm edit cho project ở trang thai luu nhap
        public ActionResult Edit(int id, int StaId)
        {
            if (StaId == 2)
            {
                var product = modeldb.PROPERTies.FirstOrDefault(x => x.ID == id);
                ViewBag.Type = modeldb.PROPERTY_TYPE.ToList();
                ListItem();
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Agency", new { userid = idd });
            }

        }





        [HttpPost]
        public ActionResult Edit(int id, PROPERTY p, string submit)
        {
            int ID = id;

            ListItem();
            var en = modeldb.PROPERTies.Find(p.ID);

            //single image
            var PROPERTY = modeldb.PROPERTies.FirstOrDefault(x => x.ID == id);
            ViewBag.Type = modeldb.PROPERTY_TYPE.ToList();
            if (p.AvatarFile != null && p.AvatarFile.ContentLength > 0)
            {
                if (Path.GetExtension(p.AvatarFile.FileName).ToLower() == ".jpg"
                    || Path.GetExtension(p.AvatarFile.FileName).ToLower() == ".png"
                    || Path.GetExtension(p.AvatarFile.FileName).ToLower() == ".gif"
                    || Path.GetExtension(p.AvatarFile.FileName).ToLower() == ".jpeg")
                {
                    string filename = Path.GetFileNameWithoutExtension(p.AvatarFile.FileName);
                    string extention = Path.GetExtension(p.AvatarFile.FileName);

                    filename = filename + DateTime.Now.ToString("yymmfff") + extention;
                    p.Avatar = filename;

                    filename = Path.Combine(Server.MapPath("~/Images"), filename);

                    p.AvatarFile.SaveAs(filename);
                    PROPERTY.Avatar = p.Avatar;
                }
            }

            PROPERTY.ID = p.ID;
            PROPERTY.PropertyName = p.PropertyName;
            PROPERTY.PropertyType_ID = p.PropertyType_ID;
            PROPERTY.Content = p.Content;
            PROPERTY.Street_ID = p.Street_ID;
            PROPERTY.Ward_ID = p.Ward_ID;
            PROPERTY.District_ID = p.District_ID;
            PROPERTY.Price = p.Price;
            PROPERTY.Area = p.Area;
            PROPERTY.BedRoom = p.BedRoom;
            PROPERTY.BathRoom = p.BathRoom;
            PROPERTY.PackingPlace = p.PackingPlace;
            if (submit == "Post")
            {
                PROPERTY.Status_ID = 1;
            }
            else if (submit == "Draf")
            {
                PROPERTY.Status_ID = 2;
            }

            PROPERTY.Note = p.Note;
            modeldb.SaveChanges();
            return RedirectToAction("Index", "Agency", new { userid = idd });



        }



        public void ListItem()
        {
            ViewBag.property_type = modeldb.PROPERTY_TYPE.ToList();
            ViewBag.feature = modeldb.FEATUREs.ToList();
            ViewBag.ward = modeldb.WARDs.OrderByDescending(x => x.ID).ToList();
            ViewBag.street = modeldb.STREETs.OrderByDescending(x => x.ID).ToList();
            ViewBag.district = modeldb.DISTRICTs.OrderByDescending(x => x.ID).ToList();
            ViewBag.user = modeldb.USERs.OrderByDescending(x => x.ID).ToList();
            ViewBag.status = modeldb.PROJECT_STATUS.OrderByDescending(x => x.ID).ToList();

        }

        public JsonResult GetStreet(int Distric_id)
        {
            return Json(modeldb.STREETs.Where(x => x.District_ID == Distric_id)
                .Select(x => new { id = x.ID, text = x.StreetName }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWard(int Distric_id)
        {
            return Json(modeldb.WARDs.Where(x => x.District_ID == Distric_id)
                .Select(x => new { id = x.ID, text = x.WardName }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}