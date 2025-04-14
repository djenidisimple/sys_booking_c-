using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookong_tiket
{
    class ListTrain
    {
        public string infoTrain;
        public string startCountryTrain;
        public string destTrain;
        public int placeTrain;

        public ListTrain(string info, string startCountry, string destination, int Capacity)
        {
            infoTrain = info;
            startCountryTrain = startCountry;
            destTrain = destination;
            placeTrain = Capacity;
        }
    }
}
