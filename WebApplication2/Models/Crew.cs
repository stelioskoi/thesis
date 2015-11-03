using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Crew
    {
        SqlConnection con = new SqlConnection();
        List<Crew> CrewList = new List<Crew>();
        public string fname { get; set; }
        public string sname { get; set; }
        public string job{ get; set; }
        public string email { get; set; }
        public string info { get; set; }
        public byte[] image { get; set; }

        string constr = ConfigurationManager.ConnectionStrings["Contacts"].ConnectionString;
        Crew p = null;

        public List<Crew> IndexDisplay()
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            using (con)
            {
                SqlCommand cmd = new SqlCommand("Select * from Crew ", con);
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    p = new Crew();
                    p.fname = Convert.ToString(rd.GetSqlValue(1));
                    p.sname = Convert.ToString(rd.GetSqlValue(2));
                    p.job = Convert.ToString(rd.GetSqlValue(3));
                    p.email = Convert.ToString(rd.GetSqlValue(4));
                    p.info = Convert.ToString(rd.GetSqlValue(5));
                    p.image = (byte[])rd[6];
                    CrewList.Add(p);

                }
            }


            return CrewList;



        }

    }





}