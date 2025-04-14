using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Booking_tiket.Controler;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Booking_tiket
{
    class GetBook : Connexion
    {
        public List<Billet> genListBookAll(int trajet) 
        {
            List<Billet> billet = new List<Billet>();
            string query = "EXEC BilletByTrajet @trajetID = " + trajet;
            using(SqlCommand cmd = new SqlCommand(query, conn.connexion))
            {
                using (SqlDataReader data = cmd.ExecuteReader()) 
                {
                    DateTime today = DateTime.Now;
                    var rand = new Random();
                    string code = "";
                    while(data.Read())
                    {
                        var b = new Billet
                        {
                            nameTrain = (string)data["NomTrain"],
                            passager = (string)data["Client"],
                            depart = (string)data["GareDepart"],
                            arriver = (string)data["GareArrivee"],
                            dateDepart = (DateTime)data["DateHeureDepart"],
                            dateArriver = (DateTime)data["dateHeureArrive"],
                            seatNumber = (string)data["NumeroPlace"],
                            price = 10000,


                            CodeBarres = Convert.ToString(data["NomTrain"])[0] + "" + today.Day + data["NumeroPlace"] + "" + code + "",
                            reference = data["NomTrain"] + "" + today.Second + "" + today.Year
                        };
                        billet.Add(b);
                    }
                }
            }
            Console.WriteLine("B : " + billet.Count);
            return billet;
        }
        public List<Billet> genListBook(List<int> idCli,List<string> sealNumbers) 
        {
            List<Billet> billet = new List<Billet>();
            int i = 0;
            string query;
            while (i < sealNumbers.Count) 
            {
                query = "EXEC BilletByPlace @nPlace = " + sealNumbers[i] + " , @idCli = " + idCli[i] + ";";
                using (SqlCommand cmd = new SqlCommand(query, conn.connexion))
                {
                    using (SqlDataReader dataSelected = cmd.ExecuteReader())
                    {
                        DateTime today = DateTime.Now;
                        var rand = new Random();
                        string code = "";
                        while (dataSelected.Read()) 
                        {
                            byte[] bytes = new byte[5];
                            rand.NextBytes(bytes);
                            Console.WriteLine("Five random byte values:");
                            foreach (byte byteValue in bytes)
                                code += "" + byteValue;
                            var b = new Billet 
                            { 
                                nameTrain = (string)dataSelected["NomTrain"], 
                                passager = (string)dataSelected["Client"], 
                                depart  = (string)dataSelected["GareDepart"], 
                                arriver = (string)dataSelected["GareArrivee"], 
                                dateDepart = (DateTime)dataSelected["DateHeureDepart"], 
                                dateArriver = (DateTime)dataSelected["dateHeureArrive"], 
                                seatNumber = sealNumbers[i], 
                                price = 10000,


                                CodeBarres = Convert.ToString(dataSelected["NomTrain"])[0] + "" + today.Day + sealNumbers[i] + "" + code + "", 
                                reference = dataSelected["NomTrain"] + "" + today.Second + "" + today.Year
                            };
                            billet.Add(b);
                        }                    
                    }
                }
                i++;
            }
            
            return billet;
        }
        public void getBook(List<Billet> ticket) 
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Pdf file|*.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        new GeneratorPdf().GeneratorTicket(sfd.FileName, ticket);
                        Process.Start(sfd.FileName); // Ouvrir le PDF généré
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Erreur : " + e.Message);
                    }
                }
            }
        }
    }
}
