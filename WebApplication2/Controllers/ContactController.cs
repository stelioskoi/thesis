using WebApplication2.Models;
using System.Web.Mvc;
using System.IO;
using System.Web;

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
                if (f.file != null)
                {
                    f.file.SaveAs(HttpContext.Server.MapPath("~/App_Data/uploads/") + f.file.FileName);
                    f.filename = f.file.FileName;
                }
                int i = D.DataInsert("INSERT INTO table_cont(Name,Email,Comments,DocFile) VALUES ('" + f.Name + "','" + f.Email + "','" + f.Comments + "','" + f.filename + "')");
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
            return RedirectToAction("Index", "Contact");

        }


        // public void InsertContact(string name, string email, string comments)
        //{

        //}
        public ActionResult Submit()
        {
            return View();
        }

    }
}