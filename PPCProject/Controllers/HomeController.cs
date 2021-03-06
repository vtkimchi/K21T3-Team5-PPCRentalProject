﻿using System;
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
        team35Entities model = new team35Entities();
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
            var property = model.PROPERTies.ToList().Where(x => x.Status_ID == 3).OrderByDescending(x => x.ID); 
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
        public ActionResult Search(string text, string bien1, string District_ID, string Street_ID)
        {
            Function();

            var search = model.PROPERTies.Where(x => x.Status_ID == 3);

            if (text == "")
            {

                if (!string.IsNullOrEmpty(bien1))
                {
                    search = model.PROPERTies.Where(x => x.PROPERTY_TYPE.Description.Equals(bien1));

                }
                if (!string.IsNullOrEmpty(District_ID))
                {
                    int Dis_id = int.Parse(District_ID);
                    search = model.PROPERTies.Where(x => x.DISTRICT.ID == Dis_id);

                }
                if (!string.IsNullOrEmpty(Street_ID))
                {
                    int Str_id = int.Parse(Street_ID);
                    search = model.PROPERTies.Where(x => x.Street_ID == Str_id);
                }
                //if (bed != 0)
                //{
                //    search = model.PROPERTies.Where(x => x.BedRoom == bed);

                //}
                //if (bath != 0)
                //{
                //    search = model.PROPERTies.Where(x => x.BathRoom == bath);

                //}

                return View(search.ToList().Where(x => x.Status_ID == 3));                
            }
            else
            {
                search = model.PROPERTies.Where(x => x.PROPERTY_TYPE.Description.Contains(text) || x.PropertyName.Contains(text) || x.DISTRICT.DistrictName.Contains(text) || x.WARD.WardName.Contains(text) || x.STREET.StreetName.Contains(text)
                || x.BathRoom.ToString().Contains(text) || x.Area.Contains(text) || x.PROPERTY_TYPE.Description.Contains(text) || x.Price.ToString().Contains(text));
                return View(search.ToList().Where(x => x.Status_ID == 3));
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