using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
  public  class ZonaBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString + "MultipleActiveResultSets = true;";
        public List<Zona> Zonat
        {
            get
            {
                List<Zona> zo = new List<Zona>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from Zonat", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Zona z = new Zona();
                        z.id = Convert.ToInt64(reader["Id_Zona"].ToString());                        
                        z.emri = reader["Emertimi"].ToString();
                     
                        zo.Add(z);
                    }
                    conn.Close();
                    return zo;
                   
                }
                
            }
        }

        public void modifiko(Zona zona)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //modifikon zonen te tabela e zonave
                SqlCommand cmd = new SqlCommand("update Zonat set Emertimi = '" + zona.emri + "' where Id_Zona = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = zona.id;
              
               
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

        public void shto(Zona zona)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into Zonat (Emertimi) values (@emri)", conn);
               

                cmd.Parameters.Add("@emri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@emri"].Value = zona.emri;
                //insert te tabella e zonave 
                conn.Open();
                cmd.ExecuteNonQuery();

                //gjej id e zones se  futur 
                SqlCommand cmdZonaFundit = new SqlCommand("SELECT MAX (Id_Zona) AS maxzona FROM Zonat", conn);
                Int64 maxZonaId = Convert.ToInt64(cmdZonaFundit.ExecuteScalar());

                //insert te tabella e skontos me perqindje skonto 0 per te gjithe klientet

                SqlCommand cmdSelectKlienti = new SqlCommand("SELECT * FROM Klienti", conn);
                SqlCommand cmdSelectLloji = new SqlCommand("SELECT * FROM Llojet", conn);
                SqlDataReader readerKlienti = cmdSelectKlienti.ExecuteReader();

                while (readerKlienti.Read())
                {
                    SqlDataReader readerLloji = cmdSelectLloji.ExecuteReader();
                    while (readerLloji.Read())
                    {
                        SqlCommand cmdInsertSkonto = new SqlCommand("insert into Skonto (Klientiid,Llojiid,Zonaid,Perqindje_skonto) values (" + Convert.ToInt64(readerKlienti["Id_klienti"]) + "," + Convert.ToInt64(readerLloji["Id_lloji"]) + "," + maxZonaId + "," + 0 + ")", conn);

                        cmdInsertSkonto.ExecuteNonQuery();
                    }
                    readerLloji.Close();
                }
                //insert te tabela e cmimeve
                SqlDataReader readerLlojiCmimet = cmdSelectLloji.ExecuteReader();
                while (readerLlojiCmimet.Read())
                {
                    SqlCommand cmdInsertCmimet = new SqlCommand("insert into Cmimet (Id_lloji,Id_zona,Valuta,Cmimi) values (" + Convert.ToInt64(readerLlojiCmimet["Id_lloji"]) + "," + maxZonaId + ",'" + "LEK" + "'," + 0 +")", conn);

                    cmdInsertCmimet.ExecuteNonQuery();
                }



                conn.Close();
            }
        }


        public void fshi(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //fshi nga tabela e zonave
                SqlCommand cmd = new SqlCommand("delete from Zonat where Id_Zona = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = id;

                //fshi nga tabela e skontos

                SqlCommand cmdSkonto = new SqlCommand("delete from Skonto where Zonaid = @Id", conn);
                cmdSkonto.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmdSkonto.Parameters["@Id"].Value = id;

                //fshi nga tabela e cmimeve

                SqlCommand cmdCmimet = new SqlCommand("delete from Cmimet where Id_zona = @Id", conn);
                cmdCmimet.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmdCmimet.Parameters["@Id"].Value = id;

                conn.Open();
                //fshin klientin
                cmd.ExecuteNonQuery();
                //fshin klientin nga tabela e skontos
                cmdSkonto.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
}
