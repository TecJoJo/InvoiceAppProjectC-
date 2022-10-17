using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace LaskutusProject
{
    internal class db_tuotteet
    {
        public List<Tuotteet> TuotteetList { get; set; }
        private string tiedostoNimi = "db_tuotteet.json";
        public List<string> TuoteLista = new();
        public List<float> HintaList = new();
        public db_tuotteet()
        {
            TuotteetList = new List<Tuotteet>();
        }

        public void Init()
        {
            try
            {
                var raakaJson = File.ReadAllText(tiedostoNimi);
                if (raakaJson.Length > 5)
                {
                    var tuotteetDB = JsonSerializer.Deserialize<db_tuotteet>(raakaJson);
                    if (tuotteetDB != null)
                    {
                        this.TuotteetList = tuotteetDB.TuotteetList;
                    }

                }
            }
            catch
            {
                Console.WriteLine("404");
            }
        }
        public void LisaaTuote(Tuotteet uusiTuote)
        {
            TuotteetList.Add(uusiTuote);
            TallennaKanta();
        }
        public void TallennaKanta()
        {
            string json = JsonSerializer.Serialize<db_tuotteet>(this);
            File.WriteAllText(tiedostoNimi, json);
        }
        public void ListaaTuotteet()
        {

            foreach (var tuote in TuotteetList)
            {
                string valmisTuote = tuote.tuote.ToString();
                TuoteLista.Add(valmisTuote);
                float valmisHinta = tuote.hinta;
                HintaList.Add(valmisHinta);
            }
        }
    }
}