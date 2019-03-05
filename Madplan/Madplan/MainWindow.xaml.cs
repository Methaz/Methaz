using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Madplan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Ret> alleRetter = new List<Ret>();
        List<Ret> ugensRetter = new List<Ret>();

        static Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            alleRetter = HentAlleRetter();//AlleRetterHentes
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            LavMadPlan();
            alleRetter = HentAlleRetter();
            buttonIndkøb.IsEnabled = true;//Knap til indkøbsliste kan først trykkes på efter at madplan er dannet ved tryk på "Lav madplan"
        }
        public void LavMadPlan()
        {
            int randomNumber = random.Next(0, alleRetter.Count);

            //Mandag
            labelMandag.Content = alleRetter[randomNumber].navn;
            ugensRetter.Add(alleRetter[randomNumber]);
            alleRetter.RemoveAt(randomNumber);

            //Tirsdag
            randomNumber = random.Next(0, alleRetter.Count);
            labelTirsdag.Content = alleRetter[randomNumber].navn;
            ugensRetter.Add(alleRetter[randomNumber]);
            alleRetter.RemoveAt(randomNumber);

            //Onsdag
            randomNumber = random.Next(0, alleRetter.Count);
            labelOnsdag.Content = alleRetter[randomNumber].navn;
            ugensRetter.Add(alleRetter[randomNumber]);
            alleRetter.RemoveAt(randomNumber);

            //Torsdag
            randomNumber = random.Next(0, alleRetter.Count);
            labelTorsdag.Content = alleRetter[randomNumber].navn;
            ugensRetter.Add(alleRetter[randomNumber]);
            alleRetter.RemoveAt(randomNumber);

            //Fredag
            randomNumber = random.Next(0, alleRetter.Count);
            labelFredag.Content = alleRetter[randomNumber].navn;
            ugensRetter.Add(alleRetter[randomNumber]);
            alleRetter.RemoveAt(randomNumber);
        }

        private List<Ret> HentAlleRetter()
        {
            List<Ret> lokalliste = new List<Ret>();

            Ret BollerIKarry = new Ret("Boller i karry", new List<Ingrediens>());
            BollerIKarry.ingredienser.Add(new Ingrediens("Ris", 5));
            BollerIKarry.ingredienser.Add(new Ingrediens("karry", 10));
            lokalliste.Add(BollerIKarry);

            Ret Pandekager = new Ret("Pandekager", new List<Ingrediens>());
            Pandekager.ingredienser.Add(new Ingrediens("Mel", 15));
            Pandekager.ingredienser.Add(new Ingrediens("Æg", 20));
            lokalliste.Add(Pandekager);

            Ret Lasagne = new Ret("Lasagne", new List<Ingrediens>());
            Lasagne.ingredienser.Add(new Ingrediens("Pastaplader", 15));
            Lasagne.ingredienser.Add(new Ingrediens("Oksekød", 15));
            lokalliste.Add(Lasagne);

            Ret Salat = new Ret("Salat", new List<Ingrediens>());
            Salat.ingredienser.Add(new Ingrediens("Iceberg", 10));
            Salat.ingredienser.Add(new Ingrediens("Tomat", 15));
            lokalliste.Add(Salat);

            Ret Tacos = new Ret("Tacos", new List<Ingrediens>());
            Tacos.ingredienser.Add(new Ingrediens("Salsa", 15));
            Tacos.ingredienser.Add(new Ingrediens("Oksekød", 15));
            lokalliste.Add(Tacos);

            Ret Burger = new Ret("Burger", new List<Ingrediens>());
            Burger.ingredienser.Add(new Ingrediens("Bøffer", 30));
            Burger.ingredienser.Add(new Ingrediens("Burgerboller", 15));
            lokalliste.Add(Burger);



            return lokalliste;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            buttonLuk.IsEnabled = true; //"Luk" knappen bliver først aktiv efter at indkøbslisten er genereret.

            double prisIAlt = 0;
            StreamWriter writer = new StreamWriter("Indkøbsliste.txt"); //https://stackoverflow.com/questions/32747/how-do-i-get-todays-date-in-c-sharp-in-mm-dd-yyyy-format
            foreach (Ret ret in ugensRetter)
            {
                writer.WriteLine("Ret: " + ret.navn);
                for (int i = 0; i < ret.ingredienser.Count; i++)
                {
                    writer.WriteLine(ret.ingredienser[i].navn);
                    writer.WriteLine(ret.ingredienser[i].pris + " kr.");
                    prisIAlt += ret.ingredienser[i].pris;
                }
                writer.WriteLine("______________________________________________");
            }
            writer.WriteLine("Prisen for denne uges mad er: " + prisIAlt + " kr.");
            writer.Close();
            MessageBox.Show("Indkøbslisten er nu genereret!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Process.Start("Indkøbsliste.txt"); //Åbner den generede "Indkøbsliste.txt" når der trykkes på knappen.
            
            

            this.Close(); //Dernæst lukkes programmet
        }
        public class Indkøbsliste
        {
            public static string txt { get; internal set; }
        };
    }

    class Ret
    {
        public string navn;
        public List<Ingrediens> ingredienser;

        public Ret(string _navn, List<Ingrediens> _ingredienser)
        {
            navn = _navn;
            ingredienser = _ingredienser;
        }
    }
    class Ingrediens
    {
        public string navn;
        public double pris;

        public Ingrediens(string _navn, double _pris)
        {
            navn = _navn;
            pris = _pris;
        }
    }
}
