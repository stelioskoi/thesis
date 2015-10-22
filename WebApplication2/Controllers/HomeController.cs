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
           
            return View();
        }

        public ActionResult Language()
        {

            return View();
        }

        public ActionResult Partners()
        {

            Images p = new Images();
            List<Images> Li = new List<Images>();
           

            Li = p.IndexDisplay();
            ViewData["IndexDisplay"] = Li;
            return View(Li);
        }

       

    }

   

  
}