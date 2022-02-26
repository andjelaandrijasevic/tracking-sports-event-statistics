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
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Data;
namespace NBA
{
    /// <summary>
    /// Interaction logic for utakmica.xaml
    /// </summary>
    public partial class utakmica : Window
    { 
       MySqlConnection connection = conn.getConnection();
        public utakmica(int idUtakmice)
        {

            InitializeComponent();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
           
            string upit = "SELECT t.IME_TIMA AS 'IME TIMA', i.IME,i.PREZIME,s.POENI,s.FAULOVI,s.SKOKOVI,s.UKRADENE_LOPTE,s.BLOKADE FROM `utakmica` u JOIN statistika_igraca s on s.ID_UTAKMICA=u.ID_UTAMKICA join igrac i on i.ID_IGRAC=s.ID_IGRAC  join tim t ON t.ID_TIM=i.ID_TIM where u.ID_UTAMKICA=" + idUtakmice;

                connection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(upit, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                //sw.Stop();
                //Console.WriteLine("Vrijeme prikaza statistike utakmice: " + sw.ElapsedMilliseconds + " ms");
        }

       
    }
}
