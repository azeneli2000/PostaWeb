using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString;

        public List<Users> Perdoruesit
        {
            get
            {
                List<Users> perd = new List<Users>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Users s = new Users();
                        s.Id = Convert.ToInt64(reader["Id"].ToString());
                        s.perdoruesi = reader["UserId"].ToString(); ;
                        s.password = reader["Password"].ToString();
                        s.emri = reader["Emri"].ToString();
                        s.mbiemri = reader["Mbiemri"].ToString();
                        perd.Add(s);
                    }
                    return perd;
                }
            }
        }
      
     
        public void modifiko(Users user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("update users set UserId = '" + user.perdoruesi + "', Emri = '"+ user.emri + "', Mbiemri = '" + user.mbiemri +  "', Password = '" +user.password+"' where Id = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = user.Id;
                   
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close(); 
              
            }

        }
        public void shto(Users user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into Users (UserId,Emri,Mbiemri,Password) values (@perd,@emri,@mbiemri,@pass)", conn);
                cmd.Parameters.Add("@perd", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@perd"].Value = user.perdoruesi;
                cmd.Parameters.Add("@emri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@emri"].Value = user.emri;
                cmd.Parameters.Add("@mbiemri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@mbiemri"].Value = user.mbiemri;
                cmd.Parameters.Add("@pass", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@pass"].Value = user.password;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
        public void fshi(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("delete from Users where Id = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
    }
} 


    

