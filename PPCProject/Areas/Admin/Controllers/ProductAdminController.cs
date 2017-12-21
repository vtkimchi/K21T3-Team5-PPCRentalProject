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
        team35Entities model = new team35Entities();
        public ActionResult Index()
        {
            if ((Session["UserID"] != null) && (int.Parse(Session["RoleID"].ToString()) == 2))
            {
                
                    var product = model.PROPERTies.OrderByDescending(x => x.ID).ToList();
                    return View(product);
               
            }
            else
            {
                return RedirectToAction("Login", "Agency", new { area = "" });
                
            }
            
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

             try
             {
               
                // string filename = Path.GetFileNameWithoutExtension(p.AvatarFile.FileName);
                // string extension = Path.GetExtension(p.AvatarFile.FileName);
                // filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                // p.Images = filename;
                // string s = p.Images;
                // filename = Path.Combine(Server.MapPath("~/Images"), filename);
                //p.AvatarFile.SaveAs(filename);
                // entity.Images = p.Images;
                string i = UpIma(p);
                string s = UpAva(p);
                entity.Images = i;
                entity.Avatar = s;
                entity.PropertyName = p.PropertyName;
                entity.PropertyType_ID = p.PropertyType_ID;
                entity.Content = p.Content;
                //entity.Street_ID = p.Street_ID;
                //entity.Ward_ID = p.Ward_ID;
                //entity.District_ID = p.District_ID;
                entity.UnitPrice = p.UnitPrice;
                entity.Area = p.Area;
                entity.BedRoom = p.BedRoom;
                entity.BathRoom = p.BathRoom;
                entity.PackingPlace = p.PackingPlace;
                entity.UserID = p.UserID;
                entity.Status_ID = p.Status_ID;
                entity.Note = p.Note;
                entity.Updated_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                //entity.Sale_ID = p.Sale_ID;
                model.SaveChanges();
             }
             catch(Exception)
             {
                 try
                 {
                     string s = UpAva(p);
                     entity.Avatar = s;
                     entity.PropertyName = p.PropertyName;
                     entity.PropertyType_ID = p.PropertyType_ID;
                     entity.Content = p.Content;
                     //entity.Street_ID = p.Street_ID;
                     //entity.Ward_ID = p.Ward_ID;
                     //entity.District_ID = p.District_ID;
                     entity.UnitPrice = p.UnitPrice;
                     entity.Area = p.Area;
                     entity.BedRoom = p.BedRoom;
                     entity.BathRoom = p.BathRoom;
                     entity.PackingPlace = p.PackingPlace;
                     entity.UserID = p.UserID;
                     entity.Status_ID = p.Status_ID;
                     entity.Note = p.Note;
                     entity.Updated_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                     //entity.Sale_ID = p.Sale_ID;
                     model.SaveChanges();
                 }
                 catch(Exception)
                 {
                     try
                     {
                         string i = UpIma(p);
                         entity.Images = i;
                         entity.PropertyName = p.PropertyName;
                         entity.PropertyType_ID = p.PropertyType_ID;
                         entity.Content = p.Content;
                         //entity.Street_ID = p.Street_ID;
                         //entity.Ward_ID = p.Ward_ID;
                         //entity.District_ID = p.District_ID;
                         entity.UnitPrice = p.UnitPrice;
                         entity.Area = p.Area;
                         entity.BedRoom = p.BedRoom;
                         entity.BathRoom = p.BathRoom;
                         entity.PackingPlace = p.PackingPlace;
                         entity.UserID = p.UserID;
                         entity.Status_ID = p.Status_ID;
                         entity.Note = p.Note;
                         entity.Updated_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                         //entity.Sale_ID = p.Sale_ID;
                         model.SaveChanges();
                     }
                     catch(Exception)
                     {
                         
                         entity.PropertyName = p.PropertyName;
                         entity.PropertyType_ID = p.PropertyType_ID;
                         entity.Content = p.Content;
                         //entity.Street_ID = p.Street_ID;
                         //entity.Ward_ID = p.Ward_ID;
                         //entity.District_ID = p.District_ID;
                         entity.UnitPrice = p.UnitPrice;
                         entity.Area = p.Area;
                         entity.BedRoom = p.BedRoom;
                         entity.BathRoom = p.BathRoom;
                         entity.PackingPlace = p.PackingPlace;
                         entity.UserID = p.UserID;
                         entity.Status_ID = p.Status_ID;
                         entity.Note = p.Note;
                         entity.Updated_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                         //entity.Sale_ID = p.Sale_ID;
                         model.SaveChanges();
                     }
                    
                 }
                 
             }


           
            // TODO: Add insert logic here

            return RedirectToAction("Index", "ProductAdmin");
        }

      


        private string UpAva(PROPERTY p)
        {
            string filename = Path.GetFileNameWithoutExtension(p.AvatarFile.FileName);
            string extension = Path.GetExtension(p.AvatarFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            p.Avatar = filename;
            string s = p.Avatar;
            filename = Path.Combine(Server.MapPath("~/Images"), filename);
            p.AvatarFile.SaveAs(filename);
            return s;
        }
        private string UpIma(PROPERTY p)
        {
            string filename;
            string extension;
            string s="";
            string b;
            foreach (var file in p.ImageFile2)
            {

                filename = Path.GetFileNameWithoutExtension(file.FileName);
                extension = Path.GetExtension(file.FileName);
                filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                p.Images = filename;
                b = p.Images;
                s = string.Concat(s, b, ",");
                filename = Path.Combine(Server.MapPath("~/Images"), filename);
                file.SaveAs(filename);
                
            }
            return s;
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


        // Details
        [HttpGet]
       // /detail mutiImage
        public ActionResult Details(int id)
        {
            var pro = model.PROPERTies.FirstOrDefault(x => x.ID == id);
            ViewBag.Imagess = Directory.EnumerateFiles(Server.MapPath("~/MultiImages")).Select(fn => "~/MultiImages/" + Path.GetFileName(fn));
            ViewBag.features = model.PROPERTY_FEATURE.Where(x => x.Property_ID == id).ToList();
            ViewBag.Countt = model.PROPERTY_FEATURE.Where(x => x.Property_ID == id).Count();
            return View(pro);
        }

        //public ActionResult Details(int id)
        //{
        //    var product = model.PROPERTies.FirstOrDefault(x => x.ID == id);
        //    return View(product);
        //}

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