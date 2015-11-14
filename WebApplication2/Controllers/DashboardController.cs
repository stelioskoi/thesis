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

        // GET: Dashboard

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Messages()
        {
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
    }


    }
