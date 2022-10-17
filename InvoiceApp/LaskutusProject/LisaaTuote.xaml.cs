using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;


namespace LaskutusProject
{
    /// <summary>
    /// Interaction logic for LisaaTuote.xaml
    /// </summary>
    public partial class LisaaTuote : Window
    {
        public LisaaTuote()
        {

            InitializeComponent();

            var database = new db_tuotteet();

            database.Init();

            //Listataan databasen tuotteet listboxiin
            foreach (var tuote in database.TuotteetList)
            {
                tuoteBox.Items.Add(tuote.tuote + " " + tuote.hinta + "€");
            }

        }

        private void AddTuote_Click(object sender, RoutedEventArgs e)
        {
            var database = new db_tuotteet();

            var tuotteenNimi = tuoteNimi.Text;
            var tuotteenHinta = tuoteHinta.Text;


            float hintaPlaceholder = 0;
            database.Init();

            //Tarkistetaan että tuotteella on nimi
            if (tuotteenNimi.Length < 2)
            {
                MessageBox.Show("Lisää tuotteen nimi");
                return;
            }

            //Tarkistetaan onko hinta numero
            try
            {
                hintaPlaceholder = float.Parse(tuotteenHinta);
            }
            catch
            {
                MessageBox.Show("Hinnan pitää olla numero");
                return;
            }


            //Lisätään tuote databaseen
            var tuoteNimiHinta = new Tuotteet(tuotteenNimi, hintaPlaceholder);
            database.LisaaTuote(tuoteNimiHinta);
            tuoteBox.Items.Add(tuotteenNimi + " " + tuotteenHinta + "€");

        }

        private void PoistaTuotteet_Click(object sender, RoutedEventArgs e)
        {
            var database = new db_tuotteet();
            var db_tuotteet = "db_tuotteet.json";

            string msgtext = "Haluatko varmasti poistaa kaikki tuotteet?";
            string txt = "Databasen nollaus";


            //Varmistetaan kaikkien tuotteiden poisto dialogilla
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    File.WriteAllText(db_tuotteet, "");
                    tuoteBox.Items.Clear();
                    break;
                case MessageBoxResult.No:
                    //Do nothing
                    break;
                
            }
               
        }

        private void ExitLisaaTuoteWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win2 = new MainWindow();
            win2.Show();
            Close();
        }
    }
}