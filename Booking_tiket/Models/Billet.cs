using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Booking_tiket
{
    class Billet
    {
        public string nameTrain { get; set; }
        public string passager { get; set; }
        public string depart { get; set; } // Gare de depart
        public string arriver { get; set; } // Gare d'arriver
        public DateTime dateDepart { get; set; } // date de depart
        public DateTime dateArriver { get; set; } // date d'arriver
        public string seatNumber { get; set; } // place reserver
        public int price { get; set; } // prix '
        public string CodeBarres { get; set; }
        public string reference { get; set; }

    }
}
