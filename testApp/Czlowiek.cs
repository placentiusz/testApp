using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace testApp
{
    class Czlowiek
    {
       public ObjectId Id { get; set; }
       public string Imie { get; set; }
       public int Wiek { get; set; }

        public override string ToString()
        {
            return Id.ToString() + Imie.ToString() + Wiek.ToString();
        }
    }
}
