using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    [Authorize]
    public class DashboardController : Controller
    {
        public CodeDB D = new CodeDB();

        // GET: Dashboard


        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveJob(postjob f)
        {

            if (ModelState.IsValid)
            {
                

                D.Open();
                int i = D.DataInsert("INSERT INTO Job_table(Name,Req,Gen) VALUES ('" + f.Name + "','" + f.Req + "','" + f.Des + "')");
                if (i > 0)
                {
                    ModelState.AddModelError("Success", "Save Success");
                }
                else
                {
                    ModelState.AddModelError("Error", "Save Error");
                }
                
                D.Close();
            }

            
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Messages()
        {
            Conmodel p = new Conmodel(); 
            List<Conmodel> Li = new List<Conmodel>();
            Li = p.ConDisplay();
            ViewData["ConDisplay"] = Li;
            return View();
        }

        public ActionResult Jobs()
        {
            ContactModel p = new ContactModel();
            List<ContactModel> Li = new List<ContactModel>();
            Li = p.JobDisplay();
            ViewData["JobDisplay"] = Li;
            return View(Li);
        }

        public ActionResult Download(string id)
        {
            string actualPath = Server.MapPath("~/App_Data/uploads/" + id);
            return File(actualPath, "application/pdf", Server.UrlEncode(id));

        }

        public ActionResult Delete(string id, string name)
        {
            string fullPath = Request.MapPath("~/App_Data/uploads/" + name);
            System.IO.File.Delete(fullPath);
            D.Open();
            int i = D.DataDelete("DELETE FROM table_form WHERE  Email= '" + id + "' ");
            D.Close();

            if (i > 0)
            {
                ModelState.AddModelError("Success", "Save Success");
            }
            else
            {
                ModelState.AddModelError("Error", "Save Error");
            }
            return RedirectToAction("Jobs", "Dashboard");
    }

        public ActionResult DeleteMap(string id, string name)
        {
            D.Open();
            int i = D.DataDelete("DELETE FROM MapGoTable WHERE  Latitude= '" + id + "' AND Longitude='" + name + "' ");
            D.Close();

            if (i > 0)
            {
                ModelState.AddModelError("Success", "Save Success");
            }
            else
            {
                ModelState.AddModelError("Error", "Save Error");
            }
            return RedirectToAction("Map", "Dashboard");
        }

        public ActionResult DeleteMessage(string id)
        {
            D.Open();
            int i = D.DataDelete("DELETE FROM conus WHERE  Comments= '" + id + "'  ");
            D.Close();

            if (i > 0)
            {
                ModelState.AddModelError("Success", "Save Success");
            }
            else
            {
                ModelState.AddModelError("Error", "Save Error");
            }
            return RedirectToAction("Messages", "Dashboard");
        }

        public ActionResult Map()
        {
            Map p = new Map();
            List<Map> Li = new List<Map>();
            Li = p.MapDisplay();
            ViewData["MapDisplay"] = Li;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePoint(Map f)
        {
            if (ModelState.IsValid)
            {
                D.Open();   
                int j = D.DataInsert("INSERT INTO MapTable(Name,Latitude,Longitude,Description) VALUES ('" + f.Name + "','" + f.Latitude + "','" + f.Longitude + "','" + f.Description + "')");
                
                if (j > 0)
                {
                    ModelState.AddModelError("Success", "Save Success");
                }
                else
                {
                    ModelState.AddModelError("Error", "Save Error");
                }

                D.Close();
            }
                return RedirectToAction("More", "Home");
        }

    }


    }


    
