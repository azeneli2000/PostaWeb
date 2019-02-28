using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class KlientiBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString + "MultipleActiveResultSets = true;";
        public List<Klienti> Klientet
        {
            get
            {
                List<Klienti> klientet = new List<Klienti>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    
                    SqlCommand cmd = new SqlCommand("SELECT Id_klienti,Emri,Agjensi,Adresa,Password,Aktiv,Perdoruesi,Shenime,EmerSubjekti,PersonKontakti,Email,LlogariBankare,Id_magazina,Id_dispecheri,Detyrimi FROM Klienti  ORDER BY Emri", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    MagazinaBusiness mb = new MagazinaBusiness();
                    DispecherBusiness db = new DispecherBusiness();
                    while (reader.Read())
                    {
                        Klienti s = new Klienti();
                        s.id = Convert.ToInt64(reader["Id_klienti"].ToString());
                        //s.idSkonto = Convert.ToInt64(reader["Id_skonto"].ToString());
                        s.perdoruesi = reader["Perdoruesi"].ToString(); ;
                        s.password = reader["Password"].ToString();
                        s.adresa = reader["Adresa"].ToString();
                        s.agjensi = Convert.ToBoolean(reader["Agjensi"].ToString());
                        s.aktiv = Convert.ToBoolean(reader["Aktiv"].ToString());
                        s.emri = reader["Emri"].ToString();
                        s.shenime = reader["Shenime"].ToString();

                        s.emerSubjekti = reader["EmerSubjekti"].ToString();
                        s.personKontakti = reader["PersonKontakti"].ToString();
                        s.email = reader["Email"].ToString();
                        s.llogBankare = reader["LlogariBankare"].ToString();
                        //s.emerDispecheri = reader["EmriDispecheri"].ToString();
                        //s.emerMagazina = reader["EmriMagazina"].ToString();

                        if (reader["Id_magazina"].ToString() != "0")
                        {
                            s.idMagazina = Convert.ToInt64(reader["Id_magazina"].ToString());
                            s.emerMagazina = mb.Magazinat.Single(a => a.id == s.idMagazina).emri.ToString();
                        }
                        if (reader["Id_dispecheri"].ToString() != "0")
                        {
                            s.idDispecheri = Convert.ToInt64(reader["Id_dispecheri"].ToString());
                          s.emerDispecheri = db.Dispecherat.Single(a => a.id == s.idDispecheri).emri.ToString();
                        }
                        klientet.Add(s);
                        s.Detyrimi = Convert.ToDecimal(reader["Detyrimi"].ToString());
                    }
                    return klientet;
                }
            }
        }

        public void modifiko(Klienti klienti)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("update Klienti set Emri = '" + klienti.emri + "', Adresa = '" + klienti.adresa + "', Perdoruesi = '" + klienti.perdoruesi + "', Password = '" + klienti.password + "', Agjensi = '" + klienti.agjensi + "', Aktiv = '" + klienti.aktiv + "', Shenime = '" + klienti.shenime + "',EmerSubjekti = '" + klienti.emerSubjekti + "',PersonKontakti = '" + klienti.personKontakti + "',Email = '" + klienti.email + "',LlogariBankare = '" + klienti.llogBankare + "',Id_magazina = " +  klienti.idMagazina + ",Id_dispecheri = " + klienti.idDispecheri +  " where Id_klienti = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = klienti.id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

        public void shto(Klienti klienti)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into Klienti (Emri,Adresa,Perdoruesi,Password,Agjensi,Aktiv,Shenime,EmerSubjekti,PersonKontakti,Email,LlogariBankare,Id_magazina,Id_dispecheri,Detyrimi) values (@emri,@adresa,@perd,@pass,@agjensi,@aktiv,@shenime,@emersubjekti,@personkontakti,@email,@llogaribankare,@idmagazina,@iddispecheri,@detyrimi)", conn);
                cmd.Parameters.Add("@perd", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@perd"].Value = klienti.perdoruesi;

                cmd.Parameters.Add("@emri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@emri"].Value = klienti.emri;

                cmd.Parameters.Add("@adresa", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@adresa"].Value = klienti.adresa;

                cmd.Parameters.Add("@pass", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@pass"].Value = klienti.password;

                cmd.Parameters.Add("@agjensi", System.Data.SqlDbType.Bit);
                cmd.Parameters["@agjensi"].Value = klienti.agjensi;

                cmd.Parameters.Add("@aktiv", System.Data.SqlDbType.Bit);
                cmd.Parameters["@aktiv"].Value =true;

                cmd.Parameters.Add("@shenime", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@shenime"].Value =(object)klienti.shenime ?? DBNull.Value;

                cmd.Parameters.Add("@emersubjekti", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@emersubjekti"].Value = (object)klienti.emerSubjekti ?? DBNull.Value;

                cmd.Parameters.Add("@personkontakti", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@personkontakti"].Value = (object)klienti.personKontakti ?? DBNull.Value;

                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@email"].Value = (object)klienti.email ?? DBNull.Value;

                cmd.Parameters.Add("@llogaribankare", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@llogaribankare"].Value = (object)klienti.llogBankare ?? DBNull.Value;

                cmd.Parameters.Add("@idmagazina", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@idmagazina"].Value = (object)klienti.idMagazina ?? DBNull.Value;

                cmd.Parameters.Add("@iddispecheri", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@iddispecheri"].Value = (object)klienti.idDispecheri ?? DBNull.Value;

                cmd.Parameters.Add("@detyrimi", System.Data.SqlDbType.Decimal);
                cmd.Parameters["@detyrimi"].Value = 0;
                conn.Open();
                cmd.ExecuteNonQuery();
                if (klienti.agjensi == false)
                {
                    //gjej id e klientit te futur 
                    SqlCommand cmdKlientiFundit = new SqlCommand("SELECT MAX (Id_klienti) AS maxklienti FROM Klienti", conn);
                    Int64 maxKlientId = Convert.ToInt64(cmdKlientiFundit.ExecuteScalar());

                    //insert te tabella e skontos me perqindje skonto 0

                    SqlCommand cmdSelectZona = new SqlCommand("SELECT * FROM Zonat", conn);
                    SqlCommand cmdSelectLloji = new SqlCommand("SELECT * FROM Llojet", conn);
                    SqlDataReader readerZona = cmdSelectZona.ExecuteReader();

                    while (readerZona.Read())
                    {
                        SqlDataReader readerLloji = cmdSelectLloji.ExecuteReader();
                        while (readerLloji.Read())
                        {
                            SqlCommand cmdInsertSkonto = new SqlCommand("insert into Skonto (Klientiid,Llojiid,Zonaid,Perqindje_skonto) values (" + maxKlientId + "," + Convert.ToInt64(readerLloji["Id_lloji"]) + "," + Convert.ToInt64(readerZona["Id_Zona"]) + "," + 0 + ")", conn);

                            cmdInsertSkonto.ExecuteNonQuery();
                        }
                        readerLloji.Close();
                    }
                }
                    conn.Close();
            }
        }


      


        public void fshi(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //nqs ska order to do......
                SqlCommand cmd = new SqlCommand("delete from Klienti where Id_klienti = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = id;


                SqlCommand cmdSkonto = new SqlCommand("delete from Skonto where Klientiid = @Id", conn);
                cmdSkonto.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmdSkonto.Parameters["@Id"].Value = id;

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
