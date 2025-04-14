using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_tiket
{
    public static class ValuePassed
    {
        public static int idTrajet;
        public static int idTrain;
        public static List<string> place = new List<string>();
        public static List<int> idPlace = new List<int>();
        public static string listToString()
        {
            string listPlace = "";
            for (int i = 0; i < ValuePassed.place.Count; i++)
            {
                if (i > 0)
                    listPlace += " , ";
                listPlace += ValuePassed.place[i];
            }
            return listPlace;
        }
    }
}
