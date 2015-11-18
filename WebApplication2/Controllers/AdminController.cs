using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        public CodeDB D = new CodeDB();

        // GET: Admin
        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User f)
        {
          
            if (ModelState.IsValid)
            {
                string user = f.UserName;
            
                D.Open();
                bool check = D.DataSearch("SELECT * FROM Admtable WHERE Admin = '" + f.UserName + "' AND Password= '" + f.Password + "' ");
                D.Close();
                if (check == true)
                {

                    FormsAuthentication.SetAuthCookie(f.UserName, false);
                    
                   return RedirectToAction("Index", "Dashboard");

                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
    }
}