using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebApplication2.Models;
using WebApplication2;
using System.Data;
using System.IO;
using System.Drawing;

namespace WebApplication2.Controllers
{
    
    public class HomeController : Controller
    {
        public CodeDB D = new CodeDB();
        

        public ActionResult Index()
        {

            return View();

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

        public ActionResult More()
        {

            Map p = new Map();
            List<Map> Li = new List<Map>();


            Li = p.MapDisplay();
            ViewData["MapDisplay"] = Li;
            return View(Li);
        }

        public ActionResult Language()
        {

            return View();
        }

        public ActionResult History()
        {

           
            return View();
        }

        public ActionResult Partners()
        {
            Crew k = new Crew();
            List<Crew> Lic = new List<Crew>();
            Lic = k.IndexDisplay();
            ViewData["IndexDisplay"] = Lic;
            return View(Lic);
            
        }
        public ActionResult Offices()
        {

            return View();
        }



        public ActionResult Philosophy()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveData(Conmodel f)
        {

            if (ModelState.IsValid)
            {
                D.Open();
                int i = D.DataInsert("INSERT INTO conus(First,Last,Email,Phone,Comments) VALUES ('" + f.FirstName + "','" + f.LastName + "','" + f.Email + "','" + f.Phone + "','" + f.Comments + "')");
                
               
                TempData["notice"] = 1;
            }
            D.Close();
            return RedirectToAction("Offices", "Home");
        }

        public ActionResult Works()
        {
            ViewBag.Message = "Works";

            return View();
        }

    }

    
}