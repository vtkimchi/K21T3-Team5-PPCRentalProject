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
        Team35Entities model = new Team35Entities();
        //
        // GET: /Agency/
        public ActionResult Index()
        {
            var property = model.PROPERTies.ToList().OrderByDescending(x => x.ID);
            return View(property);
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
                        return RedirectToAction("Index", "Agency");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Create khong thanh cong");
                    }
                }


            }
            catch
            {

            }

            return View(pROPERTY);
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
    }
}