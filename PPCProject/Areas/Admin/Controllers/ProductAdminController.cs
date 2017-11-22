using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPCProject.Model;
using System.IO;

namespace PPCProject.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        //
        // GET: /Admin/ProductAdmin/
        Team35Entities model = new Team35Entities();
        public ActionResult Index()
        {
            var product = model.PROPERTies.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = model.PROPERTies.FirstOrDefault(x => x.ID == id);
            ViewBag.property_type = model.PROPERTY_TYPE.OrderByDescending(x => x.ID).ToList();
            ViewBag.ward = model.WARDs.OrderByDescending(x => x.ID).ToList();
            ViewBag.street = model.STREETs.OrderByDescending(x => x.ID).ToList();
            ViewBag.district = model.DISTRICTs.OrderByDescending(x => x.ID).ToList();
            ViewBag.user = model.USERs.OrderByDescending(x => x.ID).ToList();
            ViewBag.status = model.PROJECT_STATUS.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(PROPERTY p)
        {
            ListAll();
            // Images

            var entity = model.PROPERTies.Find(p.ID);

            string filename = Path.GetFileNameWithoutExtension(p.AvatarFile.FileName);
            string extension = Path.GetExtension(p.AvatarFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

            p.Images = filename;
            string s = p.Images;
            filename = Path.Combine(Server.MapPath("~/Images"), filename);
            p.AvatarFile.SaveAs(filename);
            entity.PropertyName = p.PropertyName;
            
            entity.Avatar = s;

            entity.PropertyType_ID = p.PropertyType_ID;
            entity.Content = p.Content;
            entity.Street_ID = p.Street_ID;
            entity.Ward_ID = p.Ward_ID;
            entity.District_ID = p.District_ID;
            entity.UnitPrice = p.UnitPrice;
            entity.Area = p.Area;
            entity.BedRoom = p.BedRoom;
            entity.BathRoom = p.BathRoom;
            entity.PackingPlace = p.PackingPlace;
            entity.UserID = p.UserID;
            entity.Status_ID = p.Status_ID;
            entity.Note = p.Note;
            entity.Updated_at = DateTime.Parse(DateTime.Now.ToShortDateString());
            entity.Sale_ID = p.Sale_ID;

            model.SaveChanges();


            // TODO: Add insert logic here

            return RedirectToAction("Index", "ProductAdmin");
        }

        public void ListAll()
        {
            ViewBag.property_type = model.PROPERTY_TYPE.ToList();
            ViewBag.street = model.STREETs.OrderBy(x => x.StreetName).ToList();
            ViewBag.ward = model.WARDs.OrderBy(x => x.WardName).ToList();
            ViewBag.district = model.DISTRICTs.OrderBy(x => x.DistrictName).ToList();
            ViewBag.user = model.USERs.OrderBy(x => x.FullName).ToList();
            ViewBag.status = model.PROJECT_STATUS.OrderBy(x => x.Status_Name).ToList();

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PROPERTY p, HttpPostedFileBase file)
        {
            var product = new PROPERTY();      
           
            
            product.PropertyName = p.PropertyName;
            product.Price = p.Price;
            product.Content = p.Content;
            product.Area = p.Area;
            product.BedRoom = p.BedRoom;
            product.BathRoom = p.BathRoom;
            product.Created_at = p.Created_at;
            product.Create_post = p.Create_post;
            product.Updated_at = p.Updated_at;
            model.PROPERTies.Add(product);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id) 
        {
            var product = model.PROPERTies.FirstOrDefault(x => x.ID == id);
            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")] 
        public ActionResult DeleteConfirm(int id)
        {
            var product = model.PROPERTies.FirstOrDefault(x => x.ID == id);
            model.PROPERTies.Remove(product);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = model.PROPERTies.FirstOrDefault(x => x.ID == id);
            return View(product);
        }
  
	}
}