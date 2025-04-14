using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookong_tiket
{
    class Reservation
    {
        public int idPas;
        public int idPla;
        public Reservation(int idPassagier, int idPlace) 
        {
            idPas = idPassagier;
            idPla = idPlace;
        }
    }
}
