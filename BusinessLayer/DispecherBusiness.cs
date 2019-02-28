using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
 public   class DispecherBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString;

        public List<Dispecher> Dispecherat
        {
            get
            {
                List<Dispecher> dis = new List<Dispecher>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Dispecher", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Dispecher s = new Dispecher();
                        s.id = Convert.ToInt64(reader["Id_dispecheri"].ToString());
                        s.perdoruesi = reader["PerdoruesiDispecheri"].ToString();
                        s.password = reader["PasswordDispecheri"].ToString();
                        s.emri = reader["EmriDispecheri"].ToString();
                      
                        dis.Add(s);
                    }
                    return dis;
                }
            }
        }


        public void modifiko(Dispecher dis)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("update Dispecher set PerdoruesiDispecheri = '" + dis.perdoruesi + "', EmriDispecheri = '" + dis.emri + "', PasswordDispecheri = '" + dis.password + "' where Id_dispecheri = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = dis.id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
        public void shto(Dispecher dis)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into Dispecher (PerdoruesiDispecheri,EmriDispecheri,PasswordDispecheri) values (@perd,@emri,@pass)", conn);
                cmd.Parameters.Add("@perd", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@perd"].Value = dis.perdoruesi;
                cmd.Parameters.Add("@emri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@emri"].Value = dis.emri;               
                cmd.Parameters.Add("@pass", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@pass"].Value = dis.password;
              
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
        public void fshi(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("delete from Dispecher where Id_dispecheri = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

    }
}
