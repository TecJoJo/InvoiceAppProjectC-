using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaskutusProject
{
    internal class Tuotteet
    {
        public string tuote { get; set; }  
        public float hinta { get; set; }
        public int maara { set; get; }
        public string yksikko { set; get; }
        
        public Tuotteet() { }
        public Tuotteet(string tuote, float hinta)
        {
            this.tuote = tuote;
            this.hinta = hinta;
            this.maara = 0;
            this.yksikko = string.Empty;
        }

        public Tuotteet(Tuotteet oldProduct, int m, string y)
        {
            this.tuote = oldProduct.tuote;
            this.hinta = oldProduct.hinta;
            this.maara = m;
            this.yksikko = y;
        }

        //Tulosta tuote, hinta, määrä ja yksikkö
    }
}
