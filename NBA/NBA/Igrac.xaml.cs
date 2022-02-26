using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace NBA
{
    /// <summary>
    /// Interaction logic for Igrac.xaml
    /// </summary>
    public partial class Igrac : Window
    {
        public Igrac(int skokovi,int blokade,string ime,string prezime,int visina,int godine,string pozicija,string tim,int br_poena,int br_utakmica,int faulovi,int ukradene_lopte,float PoeniUtakmica)
        {
            InitializeComponent();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            txtSkokovi.Text = skokovi.ToString();
            txtBlokade.Text = blokade.ToString();
            txtVisina.Text = visina.ToString();
            txtGodine.Text = godine.ToString();
            txtPozicija.Text = pozicija;
            txtTim.Text = tim;
            txtPoeni.Text = br_poena.ToString();
            txtOdigranoUtakm.Text = br_utakmica.ToString();
            txtFaulovi.Text = faulovi.ToString();
            txtUkradeneLopte.Text = ukradene_lopte.ToString();
            txtPoeniUtakmica.Text = Math.Round(PoeniUtakmica,1).ToString();
            this.Title = ime + " " + prezime;
            sw.Stop();
            Console.WriteLine("Vrijeme izvrsenja fdetalji o igracu: " + sw.ElapsedMilliseconds + " ms");

        }

    }
}
