using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SkontoBusiness
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString + "MultipleActiveResultSets = true;";
        public List<Skonto> Skontot
        {
            get
            {
                List<Skonto> sk = new List<Skonto>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select Klienti.Emri AS Klienti, Zonat.Emertimi AS Zona,Llojet.Emertimi as Lloji, Skonto.Klientiid,Skonto.Llojiid,Skonto.Zonaid,Skonto.Perqindje_skonto,Skonto.Id_skonto from Zonat,Llojet,Skonto,Klienti where Skonto.Zonaid = Zonat.Id_Zona AND Skonto.Llojiid= Llojet.Id_lloji AND Skonto.Klientiid = Klienti.Id_klienti", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Skonto s = new Skonto();
                        s.idLloji = Convert.ToInt64(reader["Llojiid"].ToString());
                        s.idZona = Convert.ToInt64(reader["Zonaid"].ToString());
                        s.idKlienti = Convert.ToInt64(reader["Klientiid"].ToString());

                        s.emriLloji = reader["Lloji"].ToString();
                        s.emriZona = reader["Zona"].ToString();
                        s.emriKlienti = reader["Klienti"].ToString();

                        s.skonto = Convert.ToDecimal(reader["Perqindje_skonto"]);
                       
                        s.id = Convert.ToInt64(reader["Id_skonto"].ToString());
                        sk.Add(s);
                    }
                    return sk;
                }
            }
        }

        public void modifiko(Skonto skonto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("update Skonto set Perqindje_skonto = " + skonto.skonto + " where Id_skonto = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = skonto.id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
    }
}
