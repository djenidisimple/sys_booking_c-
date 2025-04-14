using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using iTextSharp.text;

namespace Booking_tiket
{
    public static class Constant
    {
        public static string settings = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\Djenidi\documents\visual studio 2012\Projects\Booking_tiket\Booking_tiket\Train.mdf';Integrated Security=True";
        public static SqlConnection connexion;
        public static Font policeTitre = new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD);
        public static Font policeInfos = new Font(Font.FontFamily.HELVETICA, 10);
        public static Font policePetit = new Font(Font.FontFamily.HELVETICA, 8);
    }
}
