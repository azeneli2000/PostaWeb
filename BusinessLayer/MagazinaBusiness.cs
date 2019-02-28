using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
public     class MagazinaBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString + "MultipleActiveResultSets = true;";
        public List<Magazina> Magazinat
        {
            get
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
        }

        public void modifiko(Magazina magazina)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //modifikon magazinen te tabela e magazinave
                SqlCommand cmd = new SqlCommand("update Magazinat set EmriMagazina = '" + magazina.emri + "' where Id_magazina = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = magazina.id;


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

        public void shto(Magazina magazina)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into Magazinat (EmriMagazina) values (@emri)", conn);


                cmd.Parameters.Add("@emri", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@emri"].Value = magazina.emri;
                //insert te tabella e zonave 
                conn.Open();
                cmd.ExecuteNonQuery();             
                conn.Close();
            }
        }


        public void fshi(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                SqlCommand cmd = new SqlCommand("delete from Magazinat where Id_magazina = @Id", conn);
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt);
                cmd.Parameters["@Id"].Value = id;           
                conn.Open();
                //fshin magazinen
                cmd.ExecuteNonQuery();                               
                conn.Close();

            }
        }
    }
}
