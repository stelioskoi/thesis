using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace WebApplication2.Models
{
    public class ContactModel
    {

        SqlConnection con = new SqlConnection();
        List<ContactModel> JobList = new List<ContactModel>();
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Range(15, 50)]
        public int Age { get; set; }
       

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$", ErrorMessage = "Must be valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Comments { get; set; }


        [Required(ErrorMessage = "Required")]
       public HttpPostedFileBase file { get; set; }


        public string nameofjob { get; set; }
       

        string constr = ConfigurationManager.ConnectionStrings["Contacts"].ConnectionString;
        ContactModel p = null;

        public List<ContactModel> JobDisplay()
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            using (con)
            {
                SqlCommand cmd = new SqlCommand("Select * from table_form ", con);
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    
                    p = new ContactModel();
                    p.FirstName = Convert.ToString(rd.GetSqlValue(1));
                    p.LastName = Convert.ToString(rd.GetSqlValue(2));
                    p.Age = (int)rd["Age"];
                    p.Email = Convert.ToString(rd.GetSqlValue(4));
                    p.Comments = Convert.ToString(rd.GetSqlValue(5));
                    p.nameofjob = Convert.ToString(rd.GetSqlValue(6));
                    JobList.Add(p);

                }
            }


            return JobList;



        }
    }



}