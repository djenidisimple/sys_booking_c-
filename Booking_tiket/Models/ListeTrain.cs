using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_tiket.Models
{
    class ListeTrain
    {
        public string infoTrain;
        public string destTrain;
        public int placeTrain;
        public int placeOccuper;

        public ListeTrain(string info, string destination, int placeTotal, int occuper)
        {
            infoTrain = info;
            destTrain = destination;
            placeTrain = placeTotal;
            placeOccuper = occuper;
        }
    }
}
