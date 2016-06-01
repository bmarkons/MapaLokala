using HCIZadatak.Entiteti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HCIZadatak
{
    [Serializable]
    public class Lokal : INotifyPropertyChanged
    {
        private static BitmapImage noImageAvailable = new BitmapImage(new Uri(@"C:\Users\Marko\Documents\Visual Studio 2015\Projects\HCIZadatak\HCIZadatak\Images\nia300x300.svg"));

        private Point mapPoint = new Point(Double.NaN, Double.NaN);
        public Point MapPoint
        {
            get
            {
                return mapPoint;
            }

            set
            {
                mapPoint = value;
            }
        }

        #region CENE_ALKOHOL_VREDNOSTI
        // Moguce vrednosti za polje CENE i ALKOHOL
        private static List<String> rang_cena = new List<string>() { "Niske", "Srednje", "Visoke", "Luksuzne" };
        private static List<string> sluzenje_alkohola = new List<string>() { "Da", "Ne", "Do23h" };
        public static List<string> Rang_cena
        {
            get
            {
                return rang_cena;
            }
        }

        public static List<string> Sluzenje_alkohola
        {
            get
            {
                return sluzenje_alkohola;
            }
        }
        #endregion

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region NotifyProperties

        private string oznakaTxt;//validacija
        private string imeTxt;
        private string opisTxt;
        private bool hendikep;
        private bool pusenje;
        private bool rezervacije;
        private int? kapacitet;
        private string cena;
        private string alkohol;
        private string datum;
        [NonSerialized]
        private BitmapImage ikona;
        private Tip tip;
        private ObservableCollection<Etiketa> etikete = new ObservableCollection<Etiketa>();

        public string OznakaTxt
        {
            get
            {
                return oznakaTxt;
            }

            set
            {
                oznakaTxt = value;
                OnPropertyChanged("OznakaTxt");
            }
        }

        public string ImeTxt
        {
            get
            {
                return imeTxt;
            }

            set
            {
                imeTxt = value;
                OnPropertyChanged("ImeTxt");
            }
        }

        public string OpisTxt
        {
            get
            {
                return opisTxt;
            }

            set
            {
                opisTxt = value;
                OnPropertyChanged("OpisTxt");
            }
        }

        public bool Hendikep
        {
            get
            {
                return hendikep;
            }

            set
            {
                hendikep = value;
                OnPropertyChanged("Hendikep");
            }
        }

        public bool Pusenje
        {
            get
            {
                return pusenje;
            }

            set
            {
                pusenje = value;
                OnPropertyChanged("Pusenje");
            }
        }

        public bool Rezervacije
        {
            get
            {
                return rezervacije;
            }

            set
            {
                rezervacije = value;
                OnPropertyChanged("Rezervacije");
            }
        }

        public int? Kapacitet
        {
            get
            {
                return kapacitet;
            }

            set
            {
                kapacitet = value;
                OnPropertyChanged("Kapacitet");
            }
        }

        public string Cena
        {
            get
            {
                return cena;
            }

            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        public string Alkohol
        {
            get
            {
                return alkohol;
            }

            set
            {
                alkohol = value;
                OnPropertyChanged("Alkohol");
            }
        }

        public string Datum
        {
            get
            {
                return datum;
            }

            set
            {
                datum = value;
                OnPropertyChanged("Datum");
            }
        }

        public BitmapImage Ikona
        {
            get
            {
                if(ikona == null)
                {
                    if(Tip == null)
                    {
                        return noImageAvailable;
                    }
                    else
                    {
                        return Tip.Ikona;
                    }
                }
                else
                {
                    return ikona;
                }
            }

            set
            {
                ikona = value;
                OnPropertyChanged("Ikona");
            }
        }

        public Tip Tip
        {
            get
            {
                return tip;
            }

            set
            {
                tip = value;
                OnPropertyChanged("Tip");
            }
        }

        public ObservableCollection<Etiketa> Etikete
        {
            get
            {
                return etikete;
            }

            set
            {
                etikete = value;
                OnPropertyChanged("Etikete");
            }
        }
        #endregion

        #region Serialization
        private Uri imgUri;
        [OnSerializing]
        private void DecomposeIkona(StreamingContext streamContext)
        {
            if (ikona != null)
            {
                imgUri = ikona.UriSource;
            }
        }

        [OnDeserialized]
        private void ComposeIkona(StreamingContext streamContext)
        {
            if (imgUri != null)
            {
                ikona = new BitmapImage(imgUri);
            }
        }
        #endregion

        public Lokal Clone()
        {
            Lokal lokal = new Lokal();

            lokal.MapPoint = MapPoint;
            lokal.imgUri = imgUri;

            lokal.OznakaTxt = OznakaTxt;
            lokal.ImeTxt = ImeTxt;
            lokal.OpisTxt = OpisTxt;
            lokal.Hendikep = Hendikep;
            lokal.Pusenje = Pusenje;
            lokal.Rezervacije = Rezervacije;
            lokal.Kapacitet = Kapacitet;
            lokal.Cena = Cena;
            lokal.Alkohol = Alkohol;
            lokal.Datum = Datum;
            lokal.Ikona = Ikona;
            lokal.Tip = Tip;
            lokal.Etikete = Etikete;

            return lokal;
        }
    }
}
