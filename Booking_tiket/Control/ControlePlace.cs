using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Booking_tiket.Controler;
using System.Data;

namespace Booking_tiket
{
    class ControlePlace : Connexion
    {

        public ControlePlace() 
        {

        }
        public int countTrain(string query, string column) 
        {
            int nb = 0;
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read())
                    {
                        nb = Convert.ToInt32(dataSelected[column]);
                    }
                }
            }
            return nb;
        }

        public void freePlace()
        {
            
            string query = "EXEC LiberationPlaces;";
            conn.executeNonQuery(query);

        }

        public DataTable GetPlaceById(int id) 
        {
            string query = "SELECT * FROM Places WHERE TrajetID = " + id + " AND TrajetID IN (SELECT TrajetID FROM Trajets WHERE IsActive = 1)";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn.connexion);
            DataTable data= new DataTable();
            adapter.Fill(data);

            return data;
        }

        public int GetNumberPlaceById(int id)
        {
            string query = "SELECT COUNT(TrajetID) as total FROM Places WHERE TrajetID = " + id;
            int nb = countTrain(query, "total");

            return nb;
        }

        public void update(int id, int stat) 
        {
            string query = "UPDATE Places SET EstDisponible = " + stat + " WHERE PlaceID = " + id;
            conn.executeNonQuery(query);
        }

        public void listUpdate(List<int> id, int stat) 
        {
            int i = 0;
            while (i < id.Count) 
            {
                conn.open();
                update(id[i], stat);
                conn.close();
                i++;
            }
        }

        public int insertAllPlace(List<int> Place, int nbPlace) 
        {
            string query = "";
            int i = 1, j = 0, count = Place[1] - nbPlace;
            int nb_colunm = (int)(count / 4);
            string[] alpha_num = { "A", "B", "C", "D" };
            int classe = 1;
           
            if (count > 0)
            {
                while (i < count)
                {
                    if ( i % nb_colunm == 0 && i > 1) 
                    {
                        j++;
                    }
                    if (i == 20) 
                    {
                        classe++;   
                    }
                    query = "INSERT INTO Places (TrajetID, NumeroPlace, classe) VALUES( " + Place[0] + ", '" + alpha_num[j] + i + "', " + classe + ");";
                    using (SqlCommand cmdChild = new SqlCommand(query, conn.connexion))
                    {
                        cmdChild.ExecuteNonQuery();
                    }
                    i++;
                }
            }
            return count;
            
        }
        public List<List<int>> addPlace() 
        {
            List<List<int>> value = new List<List<int>>();
            string query = "SELECT Trajets.TrajetID , Trains.CapaciteTotale FROM Trajets INNER JOIN Trains ON Trains.TrainID = Trajets.TrainID WHERE Trajets.IsActive = 1;";
            int idTrajet = 0, nbPlace = 0;
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read())
                    {
                        idTrajet = (int)dataSelected["TrajetID"];
                        nbPlace = (int)dataSelected["CapaciteTotale"];
                        value.Add( new List<int> { idTrajet, nbPlace } );
                    }
                }
            }
            return value;
        }
        public void insertById(int id) 
        {
            int nbTotalTrajet = 0,i = 0, nbPlace = 0;
            string query;
            query = "SELECT COUNT(TrajetID) AS Total FROM Trajets WHERE TrajetID = " + id;
            nbTotalTrajet = countTrain(query, "Total");
            query = "SELECT COUNT(TrajetID) AS Total FROM Places WHERE TrajetID = " + id;
            nbPlace = countTrain(query, "Total");
            Console.WriteLine(nbTotalTrajet);
            if (nbPlace < nbTotalTrajet)
            {
                try
                {
                    while (i < addPlace().Count)
                    {
                        insertAllPlace(addPlace()[i], nbPlace);
                        i++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur : " + e.Message);
                }
                finally {
                    conn.close();
                }
            }
            else {
                Console.WriteLine("Enregistrement Deja existant! (fichier controlePlace)");
            }
        }
        public void insert()
        {
            int nbTotalTrajet = 0,i = 0, nbPlace = 0;
            string query;
            query = "SELECT COUNT(TrajetID) AS Total FROM Trajets WHERE IsActive = 1;";
            nbTotalTrajet = countTrain(query, "Total");
            query = "SELECT COUNT(TrajetID) AS Total FROM Places WHERE TrajetID IN (SELECT TrajetID FROM Trajets WHERE IsActive = 1);";
            nbPlace = countTrain(query, "Total");
            Console.WriteLine(nbTotalTrajet);
            if (nbPlace < nbTotalTrajet)
            {
                try
                {
                    Console.WriteLine("Ok!");
                    while (i < addPlace().Count)
                    {
                        insertAllPlace(addPlace()[i], nbPlace);
                        i++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur : " + e.Message);
                }
                finally {
                    conn.close();
                }
            }
            else {
                Console.WriteLine("Enregistrement Deja existant (Place deja complet!) ");
            }
        }
    }
}
