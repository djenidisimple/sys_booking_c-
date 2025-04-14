using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_tiket
{
    class Train
    {
        public int idTrain;
        public string nameTrain;
        public int capacityTrain;

        public Train(int id = 0, string name = null, int capacity = 0) 
        {
            idTrain = id;
            nameTrain = name;
            capacityTrain = capacity;
        }
    }
}
