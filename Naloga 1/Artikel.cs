using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naloga_1
{
    public class Artikel
    {
        public string ime;
        public double cena;
        public int zaloga;
        public string dobavitelj;

        public Artikel() { }

        public Artikel(string ime, double cena, int zaloga, string dobavitelj)
        {
            this.ime = ime;
            this.cena = cena;
            this.zaloga = zaloga;
            this.dobavitelj = dobavitelj;
        }

        public string Izpisi()
        {
            return $"Ime: {ime} | Cena: {cena} | Zaloga: {zaloga} | Dobavitelj: {dobavitelj};";
        }
    }
}