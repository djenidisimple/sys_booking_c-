using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Booking_tiket;


namespace Booking_tiket.Controler
{
    static class GetCount
    {
        private static SqlConnection _connexion;
        public static int countValue(string query, string column, string thingsFinding) 
        {
            int value = 0;
            try
            {
                _connexion = new SqlConnection(Constant.settings);
                _connexion.Open();
                using (SqlCommand cmd = new SqlCommand(query, _connexion)) 
                {
                    using (SqlDataReader dataSelected = cmd.ExecuteReader()) 
                    {
                        while (dataSelected.Read()) 
                        {
                            value = Convert.ToInt32(dataSelected[column]);
                        }
                    }
                }
            }
            catch (Exception sqlerr)
            {
                Console.WriteLine("Erreur : " + sqlerr.Message + "");
            }
            finally
            {
                _connexion.Close();
            }
            return value;
        }
    }
}
