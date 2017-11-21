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
        public ActionResult Edit(int id, PROPERTY p)
        {
            var product = model.PROPERTies.FirstOrDefault(x => x.ID == id);

            product.Avatar = p.Avatar;
            product.PropertyName = p.PropertyName;
            product.Price = p.Price;
            product.Content = p.Content;
            product.Area = p.Area;
            product.BedRoom = p.BedRoom;
            product.BathRoom = p.BathRoom;
            product.Created_at = p.Created_at;
            product.Create_post = p.Create_post;
            product.Updated_at = p.Updated_at;
            model.SaveChanges();
            return RedirectToAction("Index");
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
           
           // string ImageName = System.IO.Path.GetFileName(file.FileName);
            //string physicalPath = Server.MapPath("~/images/" + ImageName);

            // save image in folder
            //file.SaveAs(physicalPath);

            //save new record in database
        //    var allowedExtensions = new[] {  
        //    ".Jpg", ".png", ".jpg", "jpeg"  
        //}; 
        //    product.Avatar = file.ToString();
        //    var fileName = Path.GetFileName(file.FileName);
            
        //    var ext = Path.GetExtension(file.FileName);
        //    string name = Path.GetFileNameWithoutExtension(fileName);
        //    string myfile = name + "_" + product.ID + ext;
        //    var path = Path.Combine(Server.MapPath("~/Images"), myfile);
        //    product.Avatar = p.path;
           
           
            
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
          //  file.SaveAs(path);
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