using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LaskutusProject
{
    
    internal class LaskunTiedot
    {
        public string Nimi { get; set; }

        public string YTunnus { get; set; }

        public string LaskunPäivä { get; set; }

        public string LaskunEräpäivä { get; set; }

        public string Laskuttaja { get; set; }

        public string TiliNumero { get; set; }

        public string LaskuNumero { get; set; }


        public List<Tuotteet> LaskunTuotteet { set; get; }

       
        
        public LaskunTiedot()
        {
            var LaskunTuotteet = new List<Tuotteet>();
        }


        public LaskunTiedot(string nimi, string yTunnus, string laskunPäivä, string laskunEräpäivä, string laskuttaja, List<Tuotteet> ProductList, string tiliNumero,string laskuNumero)

        {
            Nimi = nimi;
            YTunnus = yTunnus;
            LaskunPäivä = laskunPäivä;
            LaskunEräpäivä = laskunEräpäivä;
            Laskuttaja = laskuttaja;
            LaskunTuotteet = ProductList;
            TiliNumero = tiliNumero;
            LaskuNumero = laskuNumero;

        }

    }
}
