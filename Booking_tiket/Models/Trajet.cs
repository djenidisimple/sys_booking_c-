using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_tiket
{
    class Trajet
    {
        public int id;
        public string gareDepart;
        public string gareArriver;
        public DateTime dateDepart;
        public DateTime dateArriver;
        public DateTime dateAllez;
        public DateTime dateRetour;
        public Trajet(int TrainID, string GareDepart, string GareArrivee, DateTime DateHeureDepart, DateTime dateHeureArrive)
        {
            id = TrainID;
            gareDepart = GareDepart;
            gareArriver = GareArrivee;
            dateDepart = DateHeureDepart;
            dateArriver = dateHeureArrive;
            dateAllez = dateArriver.AddDays(3);
            dateRetour = dateArriver.AddDays(4);

        }
    }
}
