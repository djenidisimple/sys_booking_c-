using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookong_tiket
{
    class Place
    {
        public string namePas;
        public string classePas;
        public string price;
        public int idPlace;
        public int idLo;
        
        public Place(int id, string namePassager, string classe, string pricePlace, int idLocomotive) 
        {
            idPlace = id;
            namePas = namePassager;
            classePas = classe;
            price = pricePlace;
            idLo = idLocomotive;
        }
    }
}
