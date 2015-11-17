using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class postjob
    {
        SqlConnection con = new SqlConnection();
        List<postjob> Jobs = new List<postjob>();

       
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Required")]
        public string Req { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Required")]
        public string Des { get; set; }


        string constr = ConfigurationManager.ConnectionStrings["Contacts"].ConnectionString;
        postjob p = null;

        public List<postjob> JobDisplay()
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            using (con)
            {
                SqlCommand cmd = new SqlCommand("Select * from Job_table ", con);
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    p = new postjob();
                    p.Name = Convert.ToString(rd.GetSqlValue(1));
                    p.Req = Convert.ToString(rd.GetSqlValue(2));
                    p.Des = Convert.ToString(rd.GetSqlValue(3));
                    Jobs.Add(p);

                }
                return Jobs;
            }
        }
    }
}