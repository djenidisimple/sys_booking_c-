using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_tiket
{
    class ListClient
    {
        public string infoCli;
        public string destCli;
        public string telCli;
        public string cinCli;
        public int placeCli; 
        public ListClient(string info , string telPassagier , string cinPassagier , string arriver , int nbPlace)
        {
            infoCli = info;
            telCli = telPassagier;
            cinCli = cinPassagier;
            destCli = arriver;
            placeCli = nbPlace;
        }
    }
}
