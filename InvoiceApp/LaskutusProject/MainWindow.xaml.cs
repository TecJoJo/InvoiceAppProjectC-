using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using SQLite;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Collections;
using System.IO;

namespace LaskutusProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Alustetaan lista valituille tuotteille
        List<Tuotteet> selectedProducts = new List<Tuotteet>();
        Dictionary<string, Tuotteet> productValuePairs = new Dictionary<string, Tuotteet>();
        db_laskuntiedot database = new db_laskuntiedot();

        //show states 
        

        public MainWindow()
        {


            InitializeComponent();

            //initialize the datePicker's date and it will pick today as the default value.
            datePicker.SelectedDate = DateTime.Now;
            LaskutettavaNimi.MaxLength = 34;
            var database = new db_tuotteet();

            database.Init();
            //Listataan databasen tuotteet listboxiin
            try
            {
                foreach (var tuote in database.TuotteetList)
                {
                    string listaNimike = tuote.tuote + " " + tuote.hinta + "€";
                    tuoteBox.Items.Add(listaNimike);
                    productValuePairs.Add(listaNimike, tuote);

                }
            }
            catch (ArgumentException) { return; }




        }


        private void CloseProg_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void HaeLasku_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new GetLaskuByIdWindow();
            if (File.Exists(@"./db_laskuntiedot.json"))
            {
                window1.Show();
            }
        }

        private void LisaaTuote_Click(object sender, RoutedEventArgs e)
        { 
            string msgtext = "Jos siirryt lisäämään tuotteita tietokantaan. Menetät keskeneräisen työn.\nSiirrytäänkö?";
            string txt = "";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var window1 = new LisaaTuote();
                    window1.Show();
                    Close();
                    break;
                case MessageBoxResult.No:
                    //Do nothing
                    break;

            }
        }

        private void datePicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            laskunEräpäivä.Text = DateTimeCalculation.Duedate(datePicker.SelectedDate, eräPäivä30.IsChecked);

        }


        private void TallennaLasku(object sender, RoutedEventArgs e)
        {
            MessageBoxResult yesOrNo = MessageBox.Show("Tallennetaanko lasku?", "Tallennus", MessageBoxButton.YesNo);
            if (yesOrNo == MessageBoxResult.Yes) 
            { 
                var ProductList = new List<Tuotteet>();
                var temp = new db_temp();

                string laskuNumero = DateTime.Now.Ticks.ToString();
                

                DateTime laskunPäivä = (DateTime)datePicker.SelectedDate;
                string laskuPäiväString = laskunPäivä.ToString("dd.MM.yy");

                string laskuEräpäiväString = laskunEräpäivä.Text;

                string nimiString = LaskutettavaNimi.Text;
                string ytunnusString = LaskutettavaYT.Text;
                string laskuttajaString = Laskuttaja.Text;
                string tiliNumero = Tilinumero.Text;
                foreach (var tuote in selectedProducts)
                    {
                        if (tuote != null)
                        {
                            ProductList.Add(tuote);
                        }
                    }
                
                database.Init();
                var uusiTieto = new LaskunTiedot(nimiString, ytunnusString, laskuPäiväString, laskuEräpäiväString, laskuttajaString, ProductList, tiliNumero, laskuNumero);            
                database.LisaaTieto(uusiTieto);// at this line, the laskutiedot is full-filled, and the full filled laskutiedot as an element is added to db_laskuntiedot class
                                               //(database)'s property List<laskunTiedot> Tiedot. And call TallennaKanta(),which will write all the info into the "db_laskuntiedot.json"
                temp.LisaaTieto(uusiTieto); //Lisätään nykyinen lasku temppiin josta se haetaan laskun tulostukseen
                
            }
           



        }


        private void AddTuote_Click(object sender, RoutedEventArgs e)

        {
            int aika = 0;
            string tavarat = tuoteBox.Text;
            
            try
            {
                aika = int.Parse(TuoteMaara.Text);
            }
            catch (System.FormatException) { MessageBox.Show("Arvon pitää olla numero"); }
            try
                {
                    switch (tuntikpl.SelectedIndex)
                    {
                        case 0:
                            selectedProducts.Add(new Tuotteet(productValuePairs[tavarat], aika, "h"));
                            tuoteLista.Items.Add(tavarat + " " + aika + "h");
                            break;
                        case 1:
                            selectedProducts.Add(new Tuotteet(productValuePairs[tavarat], aika, "kpl"));
                            tuoteLista.Items.Add(tavarat + " " + aika + "kpl");
                            break;
                        case >= 2:
                            MessageBox.Show("Valitse Tuote");
                            break;
                        case -1:
                            MessageBox.Show("Valitse Kappalemäärä/Tuntimäärä");
                            break;
                }
                }
            catch (System.Collections.Generic.KeyNotFoundException) { MessageBox.Show("Mene ylärivillä \"Tuotteet\" ja \"Lisää Tuote\""); }

            Debug.WriteLine(selectedProducts);
            Debug.WriteLine(productValuePairs);

        }
       
        private void RemoveTuote_Click(object sender, RoutedEventArgs e)
        {
            //remove item from the tuotelista display( remove userinterface part)
            var deleteItemIndex = tuoteLista.SelectedIndex;
            try
            {
                tuoteLista.Items.RemoveAt(deleteItemIndex);
                selectedProducts.RemoveAt(deleteItemIndex);
            }
            catch (ArgumentOutOfRangeException){}
            void showContainedItems()
            {
                foreach(var tuote in selectedProducts)
                {
                    Debug.WriteLine(tuote.tuote);
                }
            }
            Debug.WriteLine(productValuePairs);
            showContainedItems();
            
        }
        private void tuntikpl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tuntikpl.SelectedIndex)
            {

                case 0:

                    labelTuntiKpl.Text = "h";
                    break;
                case 1:
                    labelTuntiKpl.Text = "kpl";
                    break;
            }
        }



        private void AboutWindow(object sender, RoutedEventArgs e)
        {
            var window1 = new AboutWindow();
            window1.Show();

        }

        private void Neuvot(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Jos haluat luoda uuden laskun.\n\nSyötä oikeat tiedot ja valitse Tallenna/Luo Lasku\n\nVoit esikatsella haluamaasi laskua File|Hae Lasku toiminnolla\n\nJos tarvitset tuotteita laskutettavaksi\nNavigoi ylhäältä Tuotteet|Lisää Tuote");
        }

    

        private void Tulosta_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new Tulostus();
            window1.Show();
            
        }
    }

}

