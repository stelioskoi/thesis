using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Conmodel
    {

        SqlConnection con = new SqlConnection();
        List<Conmodel> Contacts = new List<Conmodel>();


        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

       [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$", ErrorMessage = "Must be valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Comments { get; set; }


        string constr = ConfigurationManager.ConnectionStrings["Contacts"].ConnectionString;
        Conmodel p = null;

        public List<Conmodel> ConDisplay()
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            using (con)
            {
                SqlCommand cmd = new SqlCommand("Select * from conus ", con);
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    p = new Conmodel();
                    p.FirstName = Convert.ToString(rd.GetSqlValue(1));
                    p.LastName = Convert.ToString(rd.GetSqlValue(2));
                    p.Email = Convert.ToString(rd.GetSqlValue(3));
                    p.Phone = Convert.ToString(rd.GetSqlValue(4));
                    p.Comments = Convert.ToString(rd.GetSqlValue(5));
                    Contacts.Add(p);

                }
                return Contacts;
            }
        }

    }
}