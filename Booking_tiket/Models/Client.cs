using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_tiket
{
    class Client
    {
        public int idCli;
        public string nameCli;
        public string cinCli;
        public string telCli;
        public string destination;

        public Client(int id, string name, string cin, string tel, string dest) 
        {
            idCli = id;
            nameCli = name;
            cinCli = cin;
            telCli = tel;
            destination = dest;
        }
    }
}
