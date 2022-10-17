using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static SQLite.SQLite3;

namespace LaskutusProject
{
    /// <summary>
    /// Interaction logic for GetLaskuByIdWindow.xaml
    /// </summary>
    public partial class GetLaskuByIdWindow : Window
    {
        db_laskuntiedot database = new db_laskuntiedot();
        
        
        public GetLaskuByIdWindow()
        {
            InitializeComponent();
            try
            {
                if (File.Exists(@"./db_laskuntiedot.json"))
                {
                    database.Init();
                    LaskuMenu.ItemsSource = database.Tiedot;
                }
                else
                {
                    MessageBox.Show("Ei olemassa olevia laskuja.");
                    Close();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Ei olemassa olevia laskuja");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Ei olemassa olevia laskuja");
            }
        }

        private void Hae_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db_temp tempDB = new db_temp();

                int laskuIndex = LaskuMenu.SelectedIndex;
                var laskunTiedot = database.Tiedot[laskuIndex];
                
                void testLasku(LaskunTiedot tieto)
                {
                    Debug.WriteLine("Asiakkaan nimi: " + tieto.Nimi + ", ");
                    Debug.WriteLine("Y-tunnus: " + tieto.YTunnus + ", ");
                    Debug.WriteLine("Laskun päivämäärä: " + tieto.LaskunPäivä + ", ");
                    Debug.WriteLine("Laskun eräpäivä: " + tieto.LaskunEräpäivä + ", ");
                    Debug.WriteLine("Laskuttaja: " + tieto.Laskuttaja + ", ");

                    Debug.WriteLine("Tilinumero: " + tieto.TiliNumero + ", ");

                    Debug.WriteLine("LaskuNumero: " + tieto.LaskuNumero + ", ");

                    Debug.Write("Tuotteet: ");
                    foreach (var tuote in tieto.LaskunTuotteet)
                    {
                        Debug.Write(tuote.tuote);
                        Debug.WriteLine(tuote.hinta);
                    }
                }

                testLasku(laskunTiedot);

                tempDB.LisaaTieto(laskunTiedot);

                var window1 = new Tulostus();
                window1.Show();

            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("lasku ei ole valittu");
            }

        }

        private void Peruuta_Click(object sender, RoutedEventArgs e)
        {
             Close();
        }

    }
}
