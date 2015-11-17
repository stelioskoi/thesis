using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class Map
    {
        SqlConnection con = new SqlConnection();
        List<Map> MapList = new List<Map>();
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Latitude { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Longitude { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }

        string constr = ConfigurationManager.ConnectionStrings["Contacts"].ConnectionString;
        Map p = null;
        public List<Map> MapDisplay()
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            using (con)
            {
                SqlCommand cmd = new SqlCommand("Select * from MapTable ", con);
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    p = new Map();
                    p.Name = Convert.ToString(rd.GetSqlValue(1));
                    p.Latitude = Convert.ToString(rd.GetSqlValue(2));
                    p.Longitude = Convert.ToString(rd.GetSqlValue(3));
                    p.Description = Convert.ToString(rd.GetSqlValue(4));
                    MapList.Add(p);

                }
            }


            return MapList;



        }
    }
}