using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class OrderBusiness
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StudentModelContext"].ConnectionString + "MultipleActiveResultSets = true;";
        public List<Order> Orders
        {
            get
            {
                List<Order> ord = new List<Order>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlstring = "SELECT Orderid ,Adresa_marresi,Pickup,Driverid_pickup,Ora_pickup,Kthyer_mag,Driverid_kthyer,Ora_kthyer_mag,Magazinaid,Dorezuar,Driverid_dorezimi,Ora_dorezimi,Paguan,Klientiid,Llojiid,Zonaid,Cmimi,Valuta,Data,Pesha,Shenime,Telefon,Zonat.Emertimi As ZonatE,Llojet.Emertimi As LlojetE,Barcode,BarCodeImage,Emri_marresi,Vlera FROM Orders,Llojet,Zonat Where YEAR(Data) = " + DateTime.UtcNow.Year + " AND MONTH(Data) = " + DateTime.UtcNow.Month + " AND DAY(Data) = " + DateTime.UtcNow.Day + " AND Orders.Llojiid = Llojet.Id_lloji AND Orders.Zonaid = Zonat.Id_Zona";

                    SqlCommand cmd = new SqlCommand(sqlstring, conn);

                    //SqlCommand cmd = new SqlCommand("SELECT * FROM Orders Where YEAR(Data) = " + DateTime.UtcNow.Year + " AND MONTH(Data) = " + DateTime.UtcNow.Month + " AND DAY(Data) = " + DateTime.UtcNow.Day, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Order o = new Order();
                        o.idOrder = Convert.ToInt64(reader["Orderid"].ToString());
                        o.adresaMarresi = reader["Adresa_marresi"].ToString();
                        o.pickUp = Convert.ToBoolean(reader["Pickup"].ToString());

                        if (reader["Driverid_pickup"].ToString() != "")
                            o.DriverIdPickUp = Convert.ToInt64(reader["Driverid_pickup"].ToString());
                        if (reader["Ora_pickup"].ToString() != "")
                            o.OraPickUp = Convert.ToDateTime(reader["Ora_pickup"].ToString());
                        if (reader["Kthyer_mag"].ToString() != "")
                            o.KthyerMag = o.pickUp = Convert.ToBoolean(reader["Kthyer_mag"].ToString());
                        if (reader["Driverid_kthyer"].ToString() != "")
                            o.DriverIdKthyerMag = Convert.ToInt64(reader["Driverid_kthyer"].ToString());
                        if (reader["Ora_kthyer_mag"].ToString() != "")
                            o.OraKthyerMag = Convert.ToDateTime(reader["Ora_kthyer_mag"].ToString());
                        if (reader["Magazinaid"].ToString() != "")
                            o.IdMagazina = Convert.ToInt64(reader["Magazinaid"].ToString());
                        o.Dorzuar = Convert.ToBoolean(reader["Dorezuar"].ToString());
                        if (reader["Driverid_dorezimi"].ToString() != "")
                            o.DriverIdDorezimi = Convert.ToInt64(reader["Driverid_dorezimi"].ToString());
                        if (reader["Ora_dorezimi"].ToString() != "")
                            o.OraDorezimi = Convert.ToDateTime(reader["Ora_dorezimi"].ToString());
                        o.Paguan = reader["Paguan"].ToString();
                        o.IdKlienti = Convert.ToInt64(reader["Klientiid"].ToString());
                        o.IdLloji = Convert.ToInt64(reader["Llojiid"].ToString());
                        o.IdZona = Convert.ToInt64(reader["Zonaid"].ToString());
                        o.Cmimi = Convert.ToDecimal(reader["Cmimi"].ToString());
                        o.Valua = reader["Valuta"].ToString();
                        o.DataOraOrder = Convert.ToDateTime(reader["Data"].ToString());
                        o.ZonaEmertimi = reader["ZonatE"].ToString();
                        o.LlojiEmertimi = reader["LlojetE"].ToString();
                        o.Telefon = reader["Telefon"].ToString();
                        o.Pesha = Convert.ToDecimal(reader["Pesha"].ToString());
                        o.EmriMarresi = reader["Emri_marresi"].ToString();
                        o.Barcode = reader["Barcode"].ToString();
                        o.ImageUrl = reader["BarCodeImage"] != null ? "data:image/jpg;base64," + Convert.ToBase64String((byte[])reader["BarCodeImage"]) : "";
                        o.Vlera = Convert.ToDecimal(reader["Vlera"].ToString());
                        ord.Add(o);
                    }
                    conn.Close();
                    return ord;
                }
            }
        }


    }
}
