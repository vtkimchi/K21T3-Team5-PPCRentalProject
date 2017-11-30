using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPCProject.Model;


namespace PPCProject.Controllers
{
    public class HomeController : Controller
    {
        List<SelectListItem> type,district, ward, street;
        Team35Entities model = new Team35Entities();
        public void Function()
        {
        /*new List*/
            type = new List<SelectListItem>();
            district = new List<SelectListItem>();
            ward = new List<SelectListItem>();
            street = new List<SelectListItem>();

        /*new variable in model*/
            var typ = model.PROPERTY_TYPE.ToList();
            var dist = model.DISTRICTs.ToList();
            var war = model.WARDs.ToList();
            var stree = model.STREETs.ToList();
        /*---------Property Type------------*/
            foreach (var n in typ)
            {
                type.Add(new SelectListItem {Text = n.Description, Value = n.Description });
            }
            ViewData["LoaiDA"] = type;
        /*---------District------------*/
            foreach (var n in dist)
            {
                district.Add(new SelectListItem { Text = n.DistrictName, Value = n.DistrictName });
            }
            ViewData["Quan"] = district;
        /*---------Ward------------*/
            foreach (var n in war)
            {
                ward.Add(new SelectListItem { Text = n.WardName, Value = n.WardName });
            }
            ViewData["Phuong"] = ward;
        /*---------Street------------*/
            foreach (var n in stree)
            {
                street.Add(new SelectListItem { Text = n.StreetName, Value = n.StreetName });
            }
            ViewData["Duong"] = street;
        }
        public ActionResult Index(int page = 1)
        {
            Function();
            List<object> ls = new List<object>();
            var property = model.PROPERTies.ToList().OrderByDescending(x => x.ID); 
            ViewBag.Page = page;
            return View(property);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            Function();
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

        public JsonResult Create(int Distric_id)
        {
            return Json(model.STREETs.Where(x => x.District_ID == Distric_id)
                .Select(x => new { id = x.ID, text = x.StreetName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Search(string text, string bien1, string District_ID, string Street_ID, string bed, string bath)
        {
            Function();

            
            if(string.IsNullOrEmpty(text))
            {
                if (!string.IsNullOrEmpty(bien1))
                {
                    var search = model.PROPERTies.ToList().Where(x => x.PROPERTY_TYPE.Description.Contains(bien1));
                    return View(search);
                }
                else if (!string.IsNullOrEmpty(District_ID))
                {
                    var search1 = model.PROPERTies.ToList().Where(x => x.DISTRICT.DistrictName.Equals(District_ID) && x.STREET.StreetName.Equals(Street_ID));
                    return View(search1);
                }
                else if (!string.IsNullOrEmpty(bed))
                {
                    var search2 = model.PROPERTies.ToList().Where(x => x.BedRoom.ToString().Equals(bed));
                    return View(search2);
                }
                else if (!string.IsNullOrEmpty(bath))
                {
                    var search3 = model.PROPERTies.ToList().Where(x => x.BathRoom.ToString().Equals(bath));
                    return View(search3);
                }
                else
                {
                    var search8 = model.PROPERTies.ToList().Where(x => x.DISTRICT.DistrictName.Contains(District_ID) && x.STREET.StreetName.Contains(Street_ID) && x.PROPERTY_TYPE.Description.Contains(bien1) && x.BedRoom.ToString().Contains(bed) && x.BathRoom.ToString().Contains(bath));
                    return View(search8);
                }
                //var search = model.PROPERTies.ToList().Where(x => x.PROPERTY_TYPE.Description.Contains(bien1) || x.DISTRICT.DistrictName.Contains(bien2) || x.STREET.StreetName.Contains(street1) || x.BedRoom.ToString().Contains(bed1) || x.BathRoom.ToString().Contains(bath1));
                //return View(search);
            }
            else 
            {
                var search5 = model.PROPERTies.ToList().Where(x =>x.PROPERTY_TYPE.Description.Contains(text) ||x.PropertyName.Contains(text) || x.DISTRICT.DistrictName.Contains(text) || x.WARD.WardName.Contains(text) || x.STREET.StreetName.Contains(text)
                || x.BathRoom.ToString().Contains(text) || x.Area.Contains(text) || x.PROPERTY_TYPE.Description.Contains(text));
                return View(search5);
            }          
            
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