using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class ShoferiBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString;

        public List<Shoferi> Shoferet
        {
            get
            {
                List<Shoferi> shof = new List<Shoferi>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Id_shoferi,UserId,Shoferet.Password,EmriShoferi,MbiemriShoferi,Shoferet.Aktiv,Id_dispecher,Shoferet.Id_klienti,Shoferet.Detyrimi,Dispecher.EmriDispecheri as disp, Klienti.Emri as kl FROM Shoferet,Dispecher,Klienti where Shoferet.Id_dispecher = Dispecher.Id_dispecheri and Shoferet.Id_klienti=Klienti.Id_klienti", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Shoferi s = new Shoferi();
                        s.Id = Convert.ToInt64(reader["Id_shoferi"].ToString());
                        s.perdoruesi = reader["UserId"].ToString();
                        s.password = reader["Password"].ToString();
                        s.emri = reader["EmriShoferi"].ToString();
                        s.mbiemri = reader["MbiemriShoferi"].ToString();
                        s.aktiv = Convert.ToBoolean(reader["Aktiv"].ToString());
                        s.IdDispecher = Convert.ToInt64(reader["Id_dispecher"].ToString());
                        s.Detyrimi = Convert.ToDecimal(reader["Detyrimi"].ToString());
                        s.EmriDispecheri = reader["disp"].ToString();
                        s.EmriKlienti = reader["kl"].ToString();
                        s.IdKlienti = Convert.ToInt64(reader["Id_klienti"].ToString());
                        shof.Add(s);
                    }
                    return shof;
                }
            }
        }


        public void modifiko(Shoferi shofer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("update Shoferet set UserId = '" + shofer.perdoruesi + "', EmriShoferi = '" + shofer.emri + "', MbiemriShoferi = '" + shofer.mbiemri + "', Password = '" + shofer.password  + "', Aktiv = '" + shofer.aktiv   + "',Detyrimi = " + shofer.Detyrimi + ", Id_dispecher = " + shofer.IdDispecher + ", Id_klienti = " + shofer.IdKlienti + " where Id_shoferi = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = shofer.Id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
        public void shto(Shoferi shof)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into Shoferet (UserId,EmriShoferi,MbiemriShoferi,Password,Aktiv,Id_dispecher,Detyrimi,Id_klienti) values (@perd,@emri,@mbiemri,@pass,@aktiv,@iddispecher,@detyrimi,@idklienti)", conn);
                cmd.Parameters.Add("@perd", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@perd"].Value = shof.perdoruesi;
                cmd.Parameters.Add("@emri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@emri"].Value = shof.emri;
                cmd.Parameters.Add("@mbiemri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@mbiemri"].Value = shof.mbiemri;
                cmd.Parameters.Add("@pass", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@pass"].Value = shof.password;
                cmd.Parameters.Add("@aktiv", System.Data.SqlDbType.Bit);
                cmd.Parameters["@aktiv"].Value = true;

                cmd.Parameters.Add("@iddispecher", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@iddispecher"].Value = shof.IdDispecher;

                cmd.Parameters.Add("@detyrimi", System.Data.SqlDbType.Decimal);
                cmd.Parameters["@detyrimi"].Value =0;

              
                cmd.Parameters.Add("@idklienti", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@idklienti"].Value = shof.IdKlienti;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
        public void fshi(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("delete from Shoferet where Id_shoferi = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
    }
}
