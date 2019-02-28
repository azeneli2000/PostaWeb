using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
  public   class Utility
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString + "MultipleActiveResultSets = true;";

        public List<Magazina> GetMagazina()
        {
            List<Magazina> ma = new List<Magazina>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Magazinat", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Magazina m = new Magazina();
                   m.id = Convert.ToInt64(reader["Id_magazina"].ToString());
                    m.emri = reader["EmriMagazina"].ToString();

                    ma.Add(m);
                }
                conn.Close();
                return ma;

            }
        }


        public List<Dispecheri> GetDispecher()
        {

            List<Dispecheri> dis = new List<Dispecheri>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Dispecher", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Dispecheri d = new Dispecheri();
                    d.id = Convert.ToInt64(reader["Id_dispecheri"].ToString());
                    d.emri = reader["EmriDispecheri"].ToString();

                    dis.Add(d);
                }
                conn.Close();
                return dis;
            }
        }

        public List<Klienti> GetKlient()
        {

            List<Klienti> kl = new List<Klienti>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Klienti", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Klienti k = new Klienti();
                    k.id = Convert.ToInt64(reader["Id_klienti"].ToString());
                    k.emri = reader["Emri"].ToString();

                    kl.Add(k);
                }
                conn.Close();
                return kl;
            }
        }

      

    }
}
