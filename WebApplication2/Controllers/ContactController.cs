﻿using WebApplication2.Models;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;

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
                bool check = D.DataSearch("SELECT * FROM table_form WHERE Email = '" + f.Email + "' ");
                D.Close();
                if (check == false)      /* provlima gia to checkkk*/
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                            file.SaveAs(path);


                            D.Open();
                            int i = D.DataInsert("INSERT INTO table_form(First_Name,Last_Name,Age,Email,Comments,File_Name,Job_Title) VALUES ('" + f.FirstName + "','" + f.LastName + "','" + f.Age + "','" + f.Email + "','" + f.Comments + "','" + file.FileName + "','" + f.nameofjob + "')");
                            if (i > 0)
                            {
                                ModelState.AddModelError("Success", "Save Success");
                            }
                            else
                            {
                                ModelState.AddModelError("Error", "Save Error");
                            }

                           
                        }

                    }

                    D.Close();
                }
                else
                {

                    TempData["check_db"] = 1;
                }
            }



            return RedirectToAction("ContactForm", "Contact");
        }




        public ActionResult Submit()
        {
            return View();
        }

    }
}