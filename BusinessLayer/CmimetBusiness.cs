using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class CmimetBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString + "MultipleActiveResultSets = true;";
        public List<Cmimet> Cmimet
        {
            get
            {
                List<Cmimet> cm = new List<Cmimet>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select Zonat.Emertimi AS Zona,Llojet.Emertimi as Lloji, Cmimet.Id_lloji,Cmimet.Id_zona,Cmimet.Cmimi,Cmimet.Valuta,Cmimet.Id_cmimi from Zonat,Llojet,Cmimet where Cmimet.Id_zona = Zonat.Id_Zona AND Cmimet.Id_lloji= Llojet.Id_lloji", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cmimet c = new Cmimet();
                        c.idLloji = Convert.ToInt64(reader["Id_lloji"].ToString());
                        c.idZona = Convert.ToInt64(reader["Id_zona"].ToString());
                        c.valuta = reader["Valuta"].ToString();
                        c.emriLloji = reader["Lloji"].ToString();
                        c.emriZona = reader["Zona"].ToString();
                        c.cmimi = Convert.ToDecimal(reader["Cmimi"]);
                        c.valuta = reader["Valuta"].ToString();
                        c.id = Convert.ToInt64(reader["Id_cmimi"].ToString());
                        cm.Add(c);
                    }
                    return cm;
                }
            }
        }

        public void modifiko(Cmimet cmimi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("update Cmimet set Cmimi = " + cmimi.cmimi + " where Id_cmimi = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = cmimi.id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
    }
}
