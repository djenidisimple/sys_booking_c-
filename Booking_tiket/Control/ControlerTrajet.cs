using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Bookong_tiket;
using System.Data;

namespace Booking_tiket.Controler
{
    class ControlerTrajet : Connexion
    {
        public ControlerTrajet()
        {

        }
        public void insert(Trajet obj)
        {
            string format = "yyyy-MM-dd HH:mm";
            string query = "INSERT INTO Trajets( TrainID , GareDepart , GareArrivee , DateHeureDepart , dateHeureArrive ) VALUES(" + obj.id + " , '" + obj.gareDepart + "' , '" + obj.gareArriver + "' , '" + obj.dateDepart.ToString(format) + "' , '" + obj.dateArriver.ToString(format) + "' )";
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public List<Trajet> fillTrajet() 
        {
            string query = "SELECT * FROM Trajets WHERE dateHeureArrive < GETDATE() AND IsActive = 1";
            List<Trajet> trajet = new List<Trajet>();
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read())
                    {
                        Trajet Trajet = new Trajet((int)dataSelected["TrainID"], (string)dataSelected["GareDepart"], (string)dataSelected["GareArrivee"], (DateTime)dataSelected["DateHeureDepart"], (DateTime)dataSelected["DateHeureArrive"]);
                        trajet.Add(Trajet);
                    }
                }
            }
            return trajet;
        }
        public void insertNewTrajet()
        {
            int i = 0;
            List<Trajet> holdTrajet = fillTrajet();
            while (i < holdTrajet.Count) 
            {
                string format = "yyyy-MM-dd HH:mm";
                string query = "INSERT INTO Trajets( TrainID , GareDepart , GareArrivee , DateHeureDepart , dateHeureArrive ) VALUES(" + holdTrajet[i].id + " , '" + holdTrajet[i].gareDepart + "' , '" + holdTrajet[i].gareArriver + "' , '" + holdTrajet[i].dateAllez.ToString(format) + "' , '" + holdTrajet[i].dateRetour.ToString(format) + "' )";
                using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
                {
                    cmd.ExecuteNonQuery();
                }
                i++;
            }
        }
        public void delete(int id)
        {
            string query = "DELETE FROM Trajets WHERE TrajetID = " + id;
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void update(Trajet obj, int id)
        {
            string format = "yyyy-MM-dd HH:mm";
            string query = "UPDATE Trajets SET TrainID = " + obj.id + " , GareDepart = '" + obj.gareDepart + "', GareArrivee = '" + obj.gareArriver + "', DateHeureDepart = '" + obj.dateDepart.ToString(format) + "', dateHeureArrive = '" + obj.dateArriver.ToString(format) + "'  WHERE TrajetID = " + id;
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public DataTable GetData()
        {
            string query = "SELECT * FROM Trajets;";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn.connexion);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
        public DataTable GetDataById(int id)
        {
            string query = "SELECT * FROM Trajets WHERE TrajetID = " + id + " WHERE IsActive = 1;";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn.connexion);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
        public List<Trajet> GetsDataById(int id)
        {
            List<Trajet> data = new List<Trajet>();
            string query = "SELECT * FROM Trajets WHERE TrajetID = " + id;
            
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read())
                    {
                        Trajet Trajet = new Trajet((int)dataSelected["TrainID"], (string)dataSelected["GareDepart"], (string)dataSelected["GareArrivee"], (DateTime)dataSelected["DateHeureDepart"], (DateTime)dataSelected["DateHeureArrive"]);
                        data.Add(Trajet);
                    }
                }
            }
            return data;
        }
    }
}
