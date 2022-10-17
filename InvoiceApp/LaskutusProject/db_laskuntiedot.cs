using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LaskutusProject
{
    internal class db_laskuntiedot
    {
        public List<LaskunTiedot> Tiedot { get; set; }
        string tiedostoNimi = "db_laskuntiedot.json";

        public db_laskuntiedot()
        {
            Tiedot = new List<LaskunTiedot>();
            
        }
        public void Init()
        {
            try
            {
                var raakaJson = File.ReadAllText(tiedostoNimi);
                if (raakaJson.Length > 5)
                {
                    var TiedotDB = JsonSerializer.Deserialize<db_laskuntiedot>(raakaJson);
                    if (TiedotDB != null)
                    {
                        this.Tiedot = TiedotDB.Tiedot;
                    }

                }
            }
            catch
            {
                Console.WriteLine("404");
            }
        }

        public void LisaaTieto(LaskunTiedot uusiTieto)
        {
            Tiedot.Add(uusiTieto);
            TallennaKanta();
        }

        public void TallennaKanta()
        {
            string json = JsonSerializer.Serialize<db_laskuntiedot>(this);
            File.WriteAllText(tiedostoNimi, json);
        }


        public void ListaaTiedot()
        {
            
            foreach(var tieto in Tiedot)
            {
                Debug.WriteLine("Asiakkaan nimi: "+tieto.Nimi + ", ");
                Debug.WriteLine("Y-tunnus: "+tieto.YTunnus + ", ");
                Debug.WriteLine("Laskun päivämäärä: "+tieto.LaskunPäivä + ", ");
                Debug.WriteLine("Laskun eräpäivä: "+tieto.LaskunEräpäivä + ", ");
                Debug.WriteLine("Laskuttaja: "+tieto.Laskuttaja + ", ");

                Debug.WriteLine("Tilinumero: " + tieto.TiliNumero + ", ");

                Debug.WriteLine("LaskuNumero: " + tieto.LaskuNumero + ", ");

                Debug.Write("Tuotteet: ");
                foreach (var tuote in tieto.LaskunTuotteet) {
                    Debug.Write(tuote.tuote);
                    Debug.WriteLine(tuote.hinta);
                        }

            }
            foreach (var tuote in Tiedot)
            {
                Debug.Write(tuote.LaskunTuotteet.Count + ", ");

                //normaalitapa
                double yhteensa = 0;
                foreach (var x in tuote.LaskunTuotteet)
                    yhteensa = yhteensa + (x.hinta * x.maara);
                Debug.WriteLine(yhteensa);

                //Debug.Write(tuote.LaskunTuotteet.Sum(a => a.maara * a.hinta).ToString()); //LinQ

            }
        }
    }

    
}
