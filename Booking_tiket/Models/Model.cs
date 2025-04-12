using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Booking_tiket
{
    class Model
    {
        public SqlConnection connexion = Constant.connexion;
        public Model() 
        {
            try
            {
                connexion = new SqlConnection(Constant.settings);
            }
            catch (Exception sqlerr)
            {
                Console.WriteLine("Erreur : " + sqlerr.Message + "");
            }
        }
        public void open() 
        {
            try
            {
                connexion.Open();
            }
            catch (Exception e) 
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
        }
        //query select data
        /*public SqlDataReader executeReader(string query) 
        {
            SqlCommand cmd = new SqlCommand(query, connexion);
            SqlDataReader dataSelected = cmd.ExecuteReader();
            return dataSelected;
        }*/
        //query insert, update, delete
        public void executeNonQuery(string query) 
        {
            using (SqlCommand cmd = new SqlCommand(query, connexion)) 
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void close() 
        {
            connexion.Close();
        }
    }
}
