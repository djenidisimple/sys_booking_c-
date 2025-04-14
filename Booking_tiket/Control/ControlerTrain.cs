using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Booking_tiket;

namespace Booking_tiket.Controler
{
    class ControlerTrain : Connexion
    {
        public ControlerTrain() 
        {
            
        }
        public void insert(Train obj) 
        {
            string query = "INSERT INTO Trains(NomTrain, CapaciteTotale) VALUES('" + obj.nameTrain + "', " + obj.capacityTrain + " )";
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void delete(int id) 
        {
            string query = "DELETE FROM Trains WHERE TrainID = " + id;
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void update(Train obj) 
        {
            string query = "UPDATE Trains SET NomTrain = '" + obj.nameTrain + "' , CapaciteTotale = " + obj.capacityTrain + " WHERE TrainID = " + obj.idTrain;
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public List<Train> viewGetId(int id) 
        {
            List<Train> data = new List<Train>();
            string query = "SELECT * FROM Trains where TrainID = " + id;
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read())
                    {
                        Train train = new Train((int)dataSelected["TrainID"], (string)dataSelected["NomTrain"], (int)dataSelected["CapaciteTotale"]);
                        data.Add(train);
                    }
                }
            }
            return data;
        } 
        public List<Train> viewAll() 
        {
            List<Train>  data = new List<Train>();
            string query = "SELECT * FROM Trains;";
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read())
                    {
                        Train train = new Train((int)dataSelected["TrainID"], (string)dataSelected["NomTrain"], (int)dataSelected["CapaciteTotale"]);
                        data.Add(train);
                    }
                }
            }
            return data;
        }
        public DataTable GetData()
        {
            string query = "SELECT TrainID, NomTrain, CapaciteTotale FROM Trains;";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn.connexion);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
        public DataTable GetDataAboutTrain(int id) 
        {
            string query = "SELECT Trains.TrainID, Trains.NomTrain, Trains.CapaciteTotale, Trajets.DateHeureDepart, Trajets.dateHeureArrive, Trajets.IsActive FROM Trajets INNER JOIN Trains ON Trains.TrainID = Trajets.TrainID WHERE Trajets.TrajetID = " + id + ";";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn.connexion);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
        public string GetGareDepart(int id) 
        {
            string value = "";
            string query = "SELECT GareDepart FROM Trajets WHERE TrajetID = " + id + ";";
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion)) 
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader()) 
                {
                    while (dataSelected.Read()) value = (string)dataSelected["GareDepart"];
                }
            }
            return value;
        }
        public string GetNameTrain(int id) 
        {
            string value = "";
            string query = "SELECT NomTrain FROM Trains WHERE TrainID = " + id + ";";
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read()) value = (string)dataSelected["NomTrain"];
                }
            }
            return value;
        }
        public int GetNumberPassager(int id) 
        {
            int nb;
            string query = "SELECT Count(PlaceID) as nb FROM Places WHERE TrajetID = " + id + " AND EstDisponible = 0;";
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read()) nb = (int)dataSelected["nb"];
                }
            }
            return 0;
        }
    }
}
