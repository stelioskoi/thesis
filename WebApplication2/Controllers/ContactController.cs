using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication2.Models;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;

namespace WebApplication2.Controllers
{
    public class ContactController : Controller
    {
       public CodeDB D = new CodeDB();

        public ActionResult Index()
        {
            

            return View();
        }




        // GET: Contact
        public ActionResult ContactForm()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDataContact(ContactModel f)
        {
            if (ModelState.IsValid)
            {
                D.Open();
                int i=D.DataInsert("INSERT INTO table_cont(Name,Email,Comments) VALUES ('" + f.Name + "','" + f.Email + "','" + f.Comments + "')");
                if (i>0)
                {
                    ModelState.AddModelError("Success","Save Success");
                }
                else
                {
                    ModelState.AddModelError("Error", "Save Error");
                }

                D.Close();
            }
            return RedirectToAction("Index", "Home");
        }


         public void InsertContact(string name, string email, string comments)
        {

        }
        public ActionResult Submit()
        {
            return View();
        }

    }
}