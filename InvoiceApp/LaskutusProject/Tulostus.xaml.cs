using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LaskutusProject
{
    /// <summary>
    /// Interaction logic for Tulostus.xaml
    /// </summary>
    public partial class Tulostus : Window
    {


        public Tulostus()
        {
            InitializeComponent();

            db_temp database = new();

            database = database.deserialization();
            

            try
            {

                foreach (var item in database.Tiedot)
                {
                    NameBlock.Text = item.Nimi;
                    laskuIDBlock.Text = item.LaskuNumero;
                    LaskunPäiväBlock.Text = item.LaskunPäivä;
                    LaskunEräpäiväBlock.Text = item.LaskunEräpäivä;
                    Laskuttaja.Text = item.Laskuttaja;
                    TiliNumeroBlock.Text = item.TiliNumero;
                    YTunnusBlock.Text = item.YTunnus;  

                    foreach (var item2 in item.LaskunTuotteet)
                    {
                        TuoteListaBox.Inlines.Add(item2.tuote + "\n");
                        HintaListaBox.Inlines.Add(item2.hinta + "\n");
                        MääräListaBox.Inlines.Add(item2.maara + "\n");
                        float yht = item2.hinta * item2.maara;
                        YhteensäListaBox.Inlines.Add(yht.ToString() + "\n");
                        AlvListaBox.Inlines.Add("24\n");
                    }
                }


            }

            catch
            {
                MessageBox.Show("Tyhjä");
            }




        }
        private void PrintLasku_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                this.IsEnabled = false;

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "Lasku");
                }

            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void BackLaskusta(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
