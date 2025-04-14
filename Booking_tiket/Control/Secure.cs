using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookong_tiket;


namespace Booking_tiket.Controler
{
    interface Secure
    {
        void writePdf(string path, List<Billet> ticket);
    }
}
