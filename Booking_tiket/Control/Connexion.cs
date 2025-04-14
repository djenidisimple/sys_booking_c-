using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking_tiket;

namespace Booking_tiket.Controler
{
    class Connexion
    {
        public Model conn;
        public Connexion() 
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
    }
}
