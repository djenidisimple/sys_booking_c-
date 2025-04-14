using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Booking_tiket.Models;

namespace Booking_tiket
{
    class ControlerReservation
    {
        public Model conn;
        public ControlerReservation() 
        {
            try
            {
                conn = new Model();
            }
            catch (Exception err)
            {
                Console.WriteLine("Erreur : " + err);
            }
        }
        public List<ListeTrain> viewsAll() 
        {
            List<ListeTrain> data = new List<ListeTrain>();
            /*string query = "SELECT LOCOMOTIVE.NomLo AS info, LOCOMOTIVE.destinationLo AS destination, LOCOMOTIVE.NbPlace AS Place,(SELECT COUNT(idPla) as Occuper FROM RESERVATION) as PlaceOcupper FROM LOCOMOTIVE;";
            using(SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using(SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while(dataSelected.Read()) 
                    {
                        ListeTrain train = new ListeTrain( Convert.ToString(dataSelected["info"]), (string)dataSelected["destination"], (int)dataSelected["Place"], (int)dataSelected["PlaceOcupper"]);
                        data.Add(train);
                    }
                }
            }*/
            return data;
        }
        public void insert(Client cli, int trajet ,List<int> place)
        {
            int i = 0;
            if (place.Count > 0) 
            {
                try
                {
                    while (i < place.Count)
                    {

                        string query = "INSERT INTO Reservation (ClientID, TrajetID, PlaceID) VALUES(" + cli.idCli + "," + trajet + "," + place[i] + ");";
                        conn.executeNonQuery(query);
                        i++;
                        Console.WriteLine("Succes Reservation!");
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Erreur : " + e.Message);
                }
                finally {
                    conn.close();
                }
            }
        }
        public void update(Client obj)
        {
            string query = "UPDATE Reservation SET  ClientID = ";
            conn.executeNonQuery(query);
        }
        public void delete(int idPas, int idPlace)
        {
            string query = "DELETE FROM RESERVATION WHERE IdPas = " + idPas + " and IdPlace = " + idPlace;
            conn.executeNonQuery(query);
        }
    }
}
