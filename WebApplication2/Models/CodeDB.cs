using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
namespace WebApplication2.Models
{
    public class CodeDB
    {    public SqlConnection con;

        //ανοίγουμε τη σύνδεση μεταξή της βάσεις και της ιστοσελίδα μέσω του Connection string
            public bool Open(string Connection = "Contacts")
        {
            con = new SqlConnection(@WebConfigurationManager.ConnectionStrings[Connection].ToString());
        try
            { bool b = true;
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                return b;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
       // κλείνουμε την σύνδεση 
public bool Close()
        {
            try
            {
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //εντολές για διάφορα καταστάσεις που θα χρειαστούμε  .
        public int ToInt(object s)
        {

            try
            {
                return Int32.Parse(s.ToString());
            }
            catch
            {
                return 0;
            }

         }

        public int DataDelete(string sql)
        {
            int LastID = 0;
            string query = sql  /*";SELECT @@Identity;"*/;
            try
            {
                if (con.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    LastID = ToInt(cmd.ExecuteScalar());
                }

                return ToInt(LastID);
            }

            catch
            {
                return 0;
            }
        }

        public int DataInsert(string sql)
        { int LastID = 0;
            string query = sql  /*";SELECT @@Identity;"*/;
            try
            {
                if (con.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    LastID = ToInt(cmd.ExecuteScalar());
                }

                return ToInt(LastID);
            }

            catch
            {
                return 0;
            }
        }

        public bool DataSearch(string sql)
        {
            
            string query = sql  /*";SELECT @@Identity;"*/;

            using (con)
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd != null)
                    {
                        rd.Close();
                        rd.Dispose();
                        return true;
                    }
                    return false;
                }
            }

            return false;



        }


    }
}
