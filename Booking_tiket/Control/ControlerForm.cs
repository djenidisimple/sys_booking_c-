using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Booking_tiket;

namespace Booking_tiket.Controler
{
    class ControlerForm : Connexion
    {
        public void Train(string type, string name, string capacity) 
        {

            int cp;
            conn.open();
            string query = "SELECT COUNT(TrainID) as count FROM Trains WHERE NomTrain = '" + name + "';";
            int exist = GetCount.countValue(query, "count", name);
            conn.close();
            if (type == "add") 
            {
                if (capacity.Length == 0 && name.Length == 0) MessageBox.Show("Veuilliez remplir tous les champs!");
                if (capacity.Length > 0 && name.Length > 0 && int.TryParse(capacity, out cp) && exist == 0)
                {
                    Train train = new Train(0, name, cp);
                    ControlerTrain dataTrain = new ControlerTrain();
                    try
                    {
                        dataTrain.conn.open();
                        dataTrain.insert(train);
                        MessageBox.Show("Enregistrement succes!");
                    }
                    catch (SqlException sqlerr)
                    {
                        MessageBox.Show("Erreur : " + sqlerr.Message);
                    }
                    finally
                    {
                        dataTrain.conn.close();
                    }
                } 
                else if (capacity.Length > 0 && name.Length > 0 && int.TryParse(capacity, out cp) && exist > 0)
                {
                    MessageBox.Show("Le nom du train existe déja!");
                }
            }
            else if (type == "edit")
            {
                ControlerTrain getValueTrain = new ControlerTrain();
                getValueTrain.conn.open();
                
                Train _train = new Train(getValueTrain.viewGetId(ValuePassed.idTrain)[0].idTrain, getValueTrain.viewGetId(ValuePassed.idTrain)[0].nameTrain, getValueTrain.viewGetId(ValuePassed.idTrain)[0].capacityTrain);
                
                getValueTrain.conn.close();

                ControlerTrain dataTrain = new ControlerTrain();
                _train.capacityTrain = Convert.ToInt32(capacity);
                _train.nameTrain = name;
                try
                {
                    dataTrain.conn.open();
                    dataTrain.update(_train);
                    MessageBox.Show("Modification reussite!");
                }
                catch (SqlException sqlerr)
                {
                    MessageBox.Show("Erreur : " + sqlerr.Message);
                }
                finally
                {
                    dataTrain.conn.close();
                }
            }
        }
        public void Trajet(Trajet trajet, string type = "add") 
        {
            DateTime today = DateTime.Now;
            DateTime tomorow = today.AddDays(1);

            DateTime dateSelectedDepart = trajet.dateDepart;
            DateTime dateSelectedArrive = trajet.dateArriver;
            int valueDepart = DateTime.Compare(dateSelectedDepart, today);
            int valueArriver = DateTime.Compare(dateSelectedArrive, tomorow);

            bool valid = trajet.id > 0 && trajet.gareDepart.Length > 0 && trajet.gareArriver.Length > 0 && trajet.dateDepart != null && valueDepart >= 0 && trajet.dateArriver != null && valueArriver >= 0;
            if (valid)
            {
                ControlerTrajet dataTrajet = new ControlerTrajet();
                dataTrajet.conn.open();
                
                try
                {
                    dataTrajet.insert(trajet);
                    ControlePlace place = new ControlePlace();
                    place.conn.open();
                    place.insert();
                    place.conn.close();
                    MessageBox.Show("Enregistrement reussite! ");
                }
                catch (SqlException sqlerr)
                {
                    MessageBox.Show("Erreur : " + sqlerr.Message);
                }
                finally
                {
                    dataTrajet.conn.close();
                }

            }
            else if (type == "edit" && trajet.id > 0 && trajet.gareDepart.Length > 0 && trajet.gareArriver.Length > 0 && trajet.dateDepart != null && trajet.dateArriver != null) 
            {
                {
                    ControlerTrajet dataTrajets = new ControlerTrajet();
                    try
                    {
                        dataTrajets.conn.open();
                        dataTrajets.update(trajet, ValuePassed.idTrajet);
                        MessageBox.Show("Modification succes!");
                    }
                    catch (SqlException sqlerr)
                    {
                        MessageBox.Show("Erreur : " + sqlerr.Message);
                    }
                    finally
                    {
                        dataTrajets.conn.close();
                    }
                }
            }
            else if (valueDepart < 0 && valueArriver < 0 && type == "add")
            {
                MessageBox.Show("La date depart du trajet doit être égale ou supérieur la date d'aujourd'hui!\nEt la date d'arriver du trajet doit être décaler de 1 jour au minimum par rapport au date de depart!");
            }
            else
            {
                MessageBox.Show("Veuilliez remplir tous les champs!");
            }
        }
        public void Reservation(string name, string mobilePhone, string cin) 
        {
            if (name.Length > 0 && mobilePhone.Length > 0 && cin.Length > 0 && ValuePassed.idPlace.Count > 0)
            {
                int lastID = 0;
                Client client = new Client(0, name, cin, mobilePhone, "Manakara");

                ControlerReservation reserved = new ControlerReservation();
                ControlerClient clientReserved = new ControlerClient();
                ControlePlace place = new ControlePlace();

                try
                {
                    clientReserved.conn.open();
                    lastID = (int)clientReserved.insert(client);
                    clientReserved.conn.close();
                    client.idCli = lastID;

                    reserved.conn.open();
                    reserved.insert(client, ValuePassed.idTrajet, ValuePassed.idPlace);
                    reserved.conn.close();

                    place.listUpdate(ValuePassed.idPlace, 0);


                }
                catch (SqlException sqlerr)
                {
                    MessageBox.Show("Erreur: " + sqlerr);
                }


                MessageBox.Show("Réservation reussite!");
            }
            else
            {
                MessageBox.Show("Veuilliez remplir tous les champs!");
            }
        }
    }
}
