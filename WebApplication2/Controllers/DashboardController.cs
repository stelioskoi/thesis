using Microsoft.Web.Helpers;
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

            postjob p = new postjob();
            List<postjob> Li = new List<postjob>();
            Li = p.JobDisplay();
            ViewData["JobDisplay"] = Li;
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
            return View();
        }

        public ActionResult Download(string id)
        {
            string actualPath = Server.MapPath("~/App_Data/uploads/" + id);
            return File(actualPath, "application/pdf", Server.UrlEncode(id));
        }
        public ActionResult DeleteApplier(ContactModel f)
        {
            string fullPath = Request.MapPath("~/App_Data/uploads/" + f.nameofcv);
            System.IO.File.Delete(fullPath);
            D.Open();
            int i = D.DataDelete("DELETE FROM table_form WHERE  Email= '" + f.Email + "' ");
            D.Close();
             if (i > 0)
            {ModelState.AddModelError("Success", "Save Success");}
            else
            {ModelState.AddModelError("Error", "Save Error");}
            return RedirectToAction("Jobs", "Dashboard");
        }

        public ActionResult DeleteMap(Map f)
        {
            //ανοίγουμε σύνδεση
            D.Open();
            //κάνουμε ερώτημα δηλαδη στη περίπτωσή μας διαγραφή από τη βάση
            int i = D.DataDelete("DELETE FROM MapTable WHERE  Latitude= '" + f.Latitude + "' AND  Longitude='" + f.Longitude + "' ");
            //κλείνουμε σύνδεση
            D.Close();

            if (i > 0)
            {
                ModelState.AddModelError("Success", "Save Success");
            }
            else
            {
                ModelState.AddModelError("Error", "Save Error");
            }
            //μεταφερόμαστε κεί που θέλουμε
            return RedirectToAction("Map", "Dashboard");
        }
         public ActionResult Map()
        {

            //παράτψ καλούμε τη λίστα απο το μοντέλο με τα στοιχεία της βάσης
            Map p = new Map();
            List<Map> Li = new List<Map>();
            Li = p.MapDisplay();
            ViewData["MapDisplay"] = Li;
            return View();
        }

        public ActionResult DeleteJob(postjob f)
        {
            D.Open();
            int i = D.DataDelete("DELETE FROM Job_table WHERE  Name= '" + f.Name + "'  ");
            D.Close();

            if (i > 0)
            {
                ModelState.AddModelError("Success", "Save Success");
            }
            else
            {
                ModelState.AddModelError("Error", "Save Error");
            }
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult DeleteMessage(Conmodel f)
        {
            D.Open();
            int i = D.DataDelete("DELETE FROM conus WHERE  Comments= '" + f.Comments + "'  ");
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

        public ActionResult DeleteEmploy(Crew f)
        {
            string fullPath = Request.MapPath("~/ImagesCrew/" + f.nameofpic);
            System.IO.File.Delete(fullPath);

            D.Open();
            int i = D.DataDelete("DELETE FROM Crew WHERE  Email= '" + f.email + "'  ");
            D.Close();

            if (i > 0)
            {
                ModelState.AddModelError("Success", "Save Success");
            }
            else
            {
                ModelState.AddModelError("Error", "Save Error");
            }
            return RedirectToAction("Crew", "Dashboard");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePoint(Map f)
        {
            if (ModelState.IsValid)
            {
                //προσθέτουμε στη βάση τα νέα στοιχεία 
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


        public ActionResult Crew()
        {

            Crew k = new Crew();
            List<Crew> Lic = new List<Crew>();
            Lic = k.IndexDisplay();
            ViewData["IndexDisplay"] = Lic;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewPartner(Crew f)
        {

            if (ModelState.IsValid)
            {
                
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                     if (file != null && file.ContentLength > 0)
                    {
                        //αποθήκευση φωτογραφίας στον φάκελο
                        f.nameofpic = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/ImagesCrew"), f.nameofpic);
                        file.SaveAs(path);
                        //ανοιγμα σύνδεσης με  βδ και καταχώρηση εγραφή
                        D.Open();
                            int i = D.DataInsert("INSERT INTO Crew(First_Name,Last_name,Job,Email,Info,ImageName) VALUES ('" + f.fname + "','" + f.sname + "','" + f.job + "','" + f.email + "','" + f.info + "','" + f.nameofpic + "')");
                             if (i > 0)
                            { ModelState.AddModelError("Success", "Save Success");}
                            else
                            { ModelState.AddModelError("Error", "Save Error");}
                            //κλείσιμο σύνδεση με βδ
                            D.Close();
                        }
                    

                    else
                    {

                        TempData["check_db"] = 1;
                    }


                }
            }

            return RedirectToAction("Partners", "Home");
        }

    


    }






}


    
