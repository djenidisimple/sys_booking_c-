using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Booking_tiket.Controler;
using Booking_tiket;

namespace Booking_tiket
{
    class ControlerClient : Connexion
    {
        public ControlerClient() 
        {   

        }
        public int insert(Client obj) 
        {
            int lastId;
            decimal id;
            string query = "INSERT INTO Client (Nom, Phone, Cin) VALUES('" + obj.nameCli + "', '" + obj.telCli + "', '" + obj.cinCli + "');SELECT SCOPE_IDENTITY();";
            using(SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                id = (decimal)cmd.ExecuteScalar();
                lastId = (int)id;
            }
            return lastId;
        }
        public DataTable GetData() 
        {
            string query = "SELECT Nom as Info, (SELECT COUNT(ClientID) FROM Reservations GROUP BY ClientID) AS nbPlace, Reservations.DateReservation FROM Client INNER JOIN Reservations ON Reservations.ClientID=Client.ClientID;";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn.connexion);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
        public int GetNumberPassager() 
        {
            int nb = 0;
            string query = "SELECT COUNT(idPas) as nb FROM PASSAGIER;";
            using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while (dataSelected.Read()) nb = (int)dataSelected["nb"];
                }
            }
            return nb;
        }
        public List<ListClient> viewsAll() 
        {
            List<ListClient> data = new List<ListClient>();
            string query = "SELECT NomPassagier as, TelPassagier as Tel, CinPassagier as cin, Arriver as destination, (SELECT COUNT(idPas) FROM Reservations GROUP BY idPas) AS nbPlace FROM PASSAGIER;";
            using(SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using(SqlDataReader dataSelected = cmd.ExecuteReader())
                {
                    while(dataSelected.Read()) 
                    {
                        ListClient train = new ListClient( Convert.ToString(dataSelected["Nom"]), (string)dataSelected["Tel"], (string)dataSelected["cin"], (string)dataSelected["destination"], (int)dataSelected["nbPlace"]);
                        data.Add(train);
                    }
                }
            }
            return data;
        }
        public void update(Client obj) 
        {
            string query = "UPDATE PASSAGIER SET NomPassagier = '" + obj.nameCli + "', TelPassagier = '"+ obj.telCli +"', CinPassagier = '" + obj.cinCli + "', Arriver = '" + obj.destination + "' WHERE idPas = " + obj.idCli;
            conn.executeNonQuery(query);
        }
        public void delete(int id) 
        {
            string query = "DELETE FROM PASSAGIER WHERE idPas = " + id;
            conn.executeNonQuery(query);
        }
    }
}
