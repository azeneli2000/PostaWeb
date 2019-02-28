using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class LlojiBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString + "MultipleActiveResultSets = true;";
        public List<Lloji> Llojet
        {
            get
            {
                List<Lloji> zo = new List<Lloji>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from Llojet", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Lloji l = new Lloji();
                        l.id = Convert.ToInt64(reader["Id_lloji"].ToString());
                        l.emri = reader["Emertimi"].ToString();

                        zo.Add(l);
                    }
                    conn.Close();
                    return zo;

                }

            }
        }

        public void modifiko(Lloji lloji)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //modifikon zonen te tabela e zonave
                SqlCommand cmd = new SqlCommand("update Llojet set Emertimi = '" + lloji.emri + "' where Id_lloji = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = lloji.id;


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

        public void shto(Lloji lloji)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //insert te tabella e llojeve 
                SqlCommand cmd = new SqlCommand("insert into Llojet (Emertimi) values (@emri)", conn);


                cmd.Parameters.Add("@emri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@emri"].Value = lloji.emri;
                
                conn.Open();
                cmd.ExecuteNonQuery();

                //gjej id e llojit te  futur 
                SqlCommand cmdLlojiFundit = new SqlCommand("SELECT MAX (Id_lloji) AS maxlloji FROM Llojet", conn);
                Int64 maxLlojiId = Convert.ToInt64(cmdLlojiFundit.ExecuteScalar());

                //insert te tabella e skontos me perqindje skonto 0 per te gjithe klientet

                SqlCommand cmdSelectKlienti = new SqlCommand("SELECT * FROM Klienti", conn);
                SqlCommand cmdSelectZona = new SqlCommand("SELECT * FROM Zonat", conn);
                SqlDataReader readerKlienti = cmdSelectKlienti.ExecuteReader();

                while (readerKlienti.Read())
                {
                    SqlDataReader readerZona = cmdSelectZona.ExecuteReader();
                    while (readerZona.Read())
                    {
                        SqlCommand cmdInsertSkonto = new SqlCommand("insert into Skonto (Klientiid,Llojiid,Zonaid,Perqindje_skonto) values (" + Convert.ToInt64(readerKlienti["Id_klienti"]) + "," + maxLlojiId + "," + Convert.ToInt64(readerZona["Id_Zona"]) + "," + 0 + ")", conn);

                        cmdInsertSkonto.ExecuteNonQuery();
                    }
                    readerZona.Close();
                }
                //insert te tabela e cmimeve
                SqlDataReader readerZonaCmimet = cmdSelectZona.ExecuteReader();
                while (readerZonaCmimet.Read())
                {
                    SqlCommand cmdInsertCmimet = new SqlCommand("insert into Cmimet (Id_lloji,Id_zona,Valuta,Cmimi) values (" + maxLlojiId + "," + Convert.ToInt64(readerZonaCmimet["Id_Zona"]) + ",'" + "LEK" + "'," + 0 + ")", conn);

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
                SqlCommand cmd = new SqlCommand("delete from Llojet where Id_lloji = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = id;

                //fshi nga tabela e skontos

                SqlCommand cmdSkonto = new SqlCommand("delete from Skonto where Llojiid = @Id", conn);
                cmdSkonto.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmdSkonto.Parameters["@Id"].Value = id;

                //fshi nga tabela e cmimeve

                SqlCommand cmdCmimet = new SqlCommand("delete from Cmimet where Id_lloji = @Id", conn);
                cmdCmimet.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmdCmimet.Parameters["@Id"].Value = id;

                conn.Open();
                //fshin klientin
                cmd.ExecuteNonQuery();
                //fshin klientin nga tabela e skontos
                cmdSkonto.ExecuteNonQuery();
                //fshin nga tabela e cmimeve
                cmdCmimet.ExecuteNonQuery();
                conn.Close();

            }
        }
    }

}

