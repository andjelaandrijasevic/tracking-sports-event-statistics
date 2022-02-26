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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Threading;
namespace NBA
{
    public partial class MainWindow : Window
    {
        MySqlConnection connection = conn.getConnection();
        string upit = "";
        int[] id_igraca = new int[1000];
        int[] nizpoena = new int[600];
        int[] nizIndeksa = new int[600];
   
        public MainWindow()
        {
            InitializeComponent();
            listBox1.FontFamily = new FontFamily("Courier New");
            lbPrethodneUtakmice.FontFamily = new FontFamily("Courier New");
            lbNaredneUtakmice.FontFamily = new FontFamily("Courier New");
            lbPretraga.FontFamily = new FontFamily("Courier New");
           // lbNaredneUtakmice.Items.Add("ime tima1" + ":" + "ime tima2");
            Stopwatch sw = new Stopwatch();
            sw.Start(); 
            upisiOdigraneUtakmice();
            upisiNeodigraneUtakmice();
            
            upisNajboljihStrijelaca();
            sw.Stop();
            Console.WriteLine("-----------\n\n\nVrijeme [MainWindow]: " + sw.ElapsedMilliseconds + " ms\n\n\n-----------");
        }

      
        private void lbPrethodneUtakmice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id_utakmice = lbPrethodneUtakmice.SelectedIndex;

                utakmica ut = new utakmica(id_utakmice+1);
                ut.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            upit = "SELECT * FROM `igrac` i ";
            for(int i=0;i<id_igraca.Length;i++)
            {
                id_igraca[i]=0;
            }
            if (txtTim.Text != "")
            {
                upit += " JOIN tim t ON i.ID_TIM=t.ID_TIM WHERE t.IME_TIMA='" + txtTim.Text+"'";
            }
            else
            {
                upit += "WHERE 1";
            }

            if ( txtIgracIme.Text != "")
            {
                upit += " AND IME='" + txtIgracIme.Text + "'";
            }
            if(txTgracPrezime.Text != "")
            {
                upit += " AND PREZIME='" + txTgracPrezime.Text + "'";
            }
           
                   
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(upit, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        string ime = reader.GetString("IME");
                        string prezime = reader.GetString("PREZIME");
                        string pozicija = reader.GetString("POZICIJA");
                        int id_igrac =Int32.Parse(reader.GetString("ID_IGRAC"));
                        //MessageBox.Show(ime + "   " + prezime + "    " +ime_tima);
                       
                        id_igraca[i] = id_igrac;
                        i++;
                        lbPretraga.Items.Add(ime+"  "+prezime+" "+pozicija);
                    }

                    connection.Close();
                 }
                catch (Exception)
                {
                    MessageBox.Show("konekcija neuspjesna");
                }
                sw.Stop();
                Console.WriteLine("-----------\n\n\nVrijeme izvrsenja trazenja: " + sw.ElapsedMilliseconds + " ms\n\n\n-----------");
        }

        void upisiOdigraneUtakmice()
        {
            //Thread t = new Thread(ThreadProcedure);

            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Greska prilikom otvaranja konekcije sa bazom [upisodigraneutakmice]!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
           //t.Start();
            //string upit = "SELECT t1.IME_TIMA 'TIM_1', t2.IME_TIMA 'TIM_2', u.REZULTAT_T1, u.REZULTAT_T2 from utakmica u JOIN tim t1 ON t1.ID_TIM = u.ID_TIM JOIN tim t2 ON t2.ID_TIM = u.ID_TIM2 WHERE u.ID_UTAMKICA<435";
            string upit = "SELECT t1.IME_TIMA 'TIM_1', t2.IME_TIMA 'TIM_2', u.REZULTAT_T1, u.REZULTAT_T2 from utakmica u JOIN tim t1 ON t1.ID_TIM = u.ID_TIM JOIN tim t2 ON t2.ID_TIM = u.ID_TIM2";    
            MySqlCommand cmd = new MySqlCommand(upit, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string ime_tima2 = reader.GetString("TIM_2");
                int rez_t2 = Int32.Parse(reader.GetString("REZULTAT_T2"));
                int rez_t1 = Int32.Parse(reader.GetString("REZULTAT_T1"));
                string ime_tima1 = reader.GetString("TIM_1");       
                //lbPrethodneUtakmice.Items.Add(ime_tima2 + "      " + rez_t2 + " : " + rez_t1 + "        " + ime_tima1);

                lbPrethodneUtakmice.Items.Add(String.Format("{0,-21}{1,4} : {2,-5}{3,-20}", ime_tima2, rez_t2, rez_t1, ime_tima1));
                    
            }
            //sw.Stop();
            //Console.WriteLine("------------------------\n\n\nVrijeme izvrsenja [upis odigrane utakmice]: " + sw.ElapsedMilliseconds + " ms\n\n\n------------------------");
            connection.Close();
            reader.Close();
            //t.Join();
        }

        void upisiNeodigraneUtakmice()
        {
           
            try
            {
                connection.Open();
                string upit = "SELECT t1.ID_TIM 'id_tim1', t1.IME_TIMA 'TIM_1', t2.IME_TIMA 'TIM_2',t2.ID_TIM 'id_tim2' from utakmica u JOIN tim t1 ON t1.ID_TIM = u.ID_TIM JOIN tim t2 ON t2.ID_TIM = u.ID_TIM2";
  
                MySqlCommand cmd = new MySqlCommand(upit, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ime_tima2 = reader.GetString("TIM_2");
                    string ime_tima1 = reader.GetString("TIM_1");
                    int idt1 = Int32.Parse(reader.GetString("id_tim1"));
                    int idt2 = Int32.Parse(reader.GetString("id_tim2"));
                    
                    lbNaredneUtakmice.Items.Add(ime_tima2 + "     " + ime_tima1);
                }
                connection.Close();
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Problem 1");
            }
        }

        private void txtTim_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbPretraga.Items.Clear();
        }

        private void txtIgrac_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbPretraga.Items.Clear();
           
        }
        private void txTgracPrezime_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbPretraga.Items.Clear();
        }

        private void lbPretraga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int visina = 0;
            int godine = 0;
            int br_poena = 0;
            int br_utakmica = 0;
            int blokade = 0;
            int faulovi = 0;
            int ukradene_lopte = 0;
            int skokovi = 0;
            string ime="", prezime="", tim="", pozicija="";
           
            if (((ListBox)sender).SelectedItem != null)
            {
                int selektovani_red = lbPretraga.SelectedIndex;
                int id = id_igraca[selektovani_red];
                connection.Open();
                string upit1 = "SELECT COUNT(DISTINCT s.id_utakmica) as br_utakmica, i.`VISINA`, i.`GODINE`, i.`POZICIJA`, t.IME_TIMA, SUM(s.POENI) AS br_poena, SUM(s.UKRADENE_LOPTE) as ukradene_lopte, SUM(s.FAULOVI) as faulovi, SUM(s.SKOKOVI) as skokovi, SUM(s.BLOKADE) as blokade,i.IME,i.PREZIME FROM `igrac` i JOIN statistika_igraca s ON s.ID_IGRAC=i.ID_IGRAC JOIN tim t ON t.ID_TIM=i.ID_TIM WHERE i.ID_IGRAC="+id;
                MySqlCommand cmd2 = new MySqlCommand(upit1, connection);
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    visina = Int32.Parse(reader2.GetString("VISINA"));
                    godine = Int32.Parse(reader2.GetString("GODINE"));
                    pozicija = reader2.GetString("POZICIJA");
                    ime = reader2.GetString("IME");
                    prezime = reader2.GetString("PREZIME");
                    tim = reader2.GetString("IME_TIMA");

                    br_poena = Int32.Parse(reader2.GetString("br_poena"));
                    ukradene_lopte = Int32.Parse(reader2.GetString("ukradene_lopte"));
                    faulovi = Int32.Parse(reader2.GetString("faulovi"));
                    skokovi = Int32.Parse(reader2.GetString("skokovi"));
                    blokade = Int32.Parse(reader2.GetString("blokade"));
                    br_utakmica = Int32.Parse(reader2.GetString("br_utakmica"));

                }
                Igrac igrac1 = new Igrac(skokovi, blokade, ime, prezime, visina, godine, pozicija, tim, br_poena, br_utakmica, faulovi, ukradene_lopte, (float)br_poena / br_utakmica);
                igrac1.Show();
                connection.Close();
                reader2.Close();
            }
        }

        private void ThreadProcedure()
        {
            using (var connecion =conn.getConnection() )
            using (var cmd = connecion.CreateCommand())
            {
                connecion.Open();
                cmd.CommandText = "SELECT t1.IME_TIMA 'TIM_1', t2.IME_TIMA 'TIM_2', u.REZULTAT_T1, u.REZULTAT_T2 from utakmica u JOIN tim t1 ON t1.ID_TIM = u.ID_TIM JOIN tim t2 ON t2.ID_TIM = u.ID_TIM2 WHERE u.ID_UTAMKICA>=435";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ime_tima2 = reader.GetString("TIM_2");
                        int rez_t2 = Int32.Parse(reader.GetString("REZULTAT_T2"));
                        int rez_t1 = Int32.Parse(reader.GetString("REZULTAT_T1"));
                        string ime_tima1 = reader.GetString("TIM_1");
                        
                        lbPrethodneUtakmice.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            lbPrethodneUtakmice.Items.Add(ime_tima2 + "      " + rez_t2 + " : " + rez_t1 + "        " + ime_tima1);
                        }));
                                               
                        
                    }
                    reader.Close();
                }
                
                connecion.Close();
            }
         }

        private void upisNajboljihStrijelaca()
        {
            
            for (int i = 0; i < 600; i++)
            {
                nizpoena[i] = 0;
            }
            connection.Open();
            string upit = "SELECT s.POENI FROM `statistika_igraca` s WHERE s.ID_IGRAC=";
            for (int i = 1; i <= 515; i++)
            {
                MySqlCommand cmd = new MySqlCommand(upit+i.ToString(), connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    nizpoena[i]=nizpoena[i]+ Int32.Parse(reader["POENI"].ToString());
                    nizIndeksa[i] = i;
                }
                reader.Close();
            }
            connection.Close();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Thread t = new Thread(ThreadProcedure1);     //-----------pozivam ThreadProcedure1----------------------//
            t.Start();

            //promjeniti dimenziju niza za sortiranje pri ukljucenju treda
            for (int i = 1; i <= 256; i++)
            {
                for (int j = i+1; j <= 257; j++)
                {
                    if (nizpoena[i] < nizpoena[j])
                    {
                        int pom = nizpoena[i];
                        nizpoena[i] = nizpoena[j];
                        nizpoena[j] = pom;
                        int pom2;
                        pom2=nizIndeksa[i];
                        nizIndeksa[i] = nizIndeksa[j];
                        nizIndeksa[j] = pom2;
                    }
                    Thread.Sleep(0); 
                }
                Thread.Sleep(0); 
            }
            t.Join();
            sw.Stop();
            Console.WriteLine("--------------\n\n\nVrijeme sortiranja je "+sw.ElapsedMilliseconds + " ms\n\n\n--------------");

            listBox1.Items.Add("RBR Ime             Prezime        PPG\n");
            connection.Open();
            upit = "SELECT i.IME, i.PREZIME FROM `igrac` i WHERE i.ID_IGRAC=";
            for (int i = 1; i <= 20; i++)
            {
                MySqlCommand cmd = new MySqlCommand(upit+nizIndeksa[i], connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ime = reader["ime"].ToString();
                    string prezime = reader["prezime"].ToString();
                    listBox1.Items.Add(String.Format("{0,-5}{1,-15}{2,-15}{3,-5}",i,ime,prezime,Math.Round(nizpoena[i] / 58.0,1).ToString()));

                   
                }
                reader.Close();
            }
            connection.Close();

        }

        private void ThreadProcedure1()
        {
            for (int i = 258; i <= 514; i++)
            {
                for (int j = i + 1; j <= 515; j++)
                {
                    if (nizpoena[i] < nizpoena[j])
                    {
                        int pom = nizpoena[i];
                        nizpoena[i] = nizpoena[j];
                        nizpoena[j] = pom;
                        int pom2;
                        pom2 = nizIndeksa[i];
                        nizIndeksa[i] = nizIndeksa[j];
                        nizIndeksa[j] = pom2;
                    }
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
            
    }
 }

        
    

