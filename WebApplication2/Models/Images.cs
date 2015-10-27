using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class Images
    {
        SqlConnection con = new SqlConnection();
        List<Images> ImagesList = new List<Images>(); 
        public string  Name { get; set; }
        public byte[] Image1 { get; set; }
        

        string constr = ConfigurationManager.ConnectionStrings["Contacts"].ConnectionString;
        Images p = null;

        public List<Images> IndexDisplay()
        {
            
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            using (con)
            {
                SqlCommand cmd = new SqlCommand("Select * from ImageTable ",con);
                SqlDataReader rd = cmd.ExecuteReader();
               
                while (rd.Read())
                {
                    p = new Images();
                    p.Name = Convert.ToString(rd.GetSqlValue(1));
                    p.Image1  = (byte[])rd[2];
                    ImagesList.Add(p);
                    
                }
            }


            return ImagesList;


        
         }  
       
    }

    

}               