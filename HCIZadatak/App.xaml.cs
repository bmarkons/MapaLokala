﻿using HCIZadatak.Entiteti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HCIZadatak
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region NotifyProperties
        private bool helpVisibility = false;
        public bool HelpVisibility
        {
            get
            {
                return helpVisibility;
            }

            set
            {
                helpVisibility = value;
                OnPropertyChanged("HelpVisibility");
            }
        }
        #endregion

        Tuple<Lokal, int> editingLokal = null;
        Tuple<Tip, int> editingTip = null;
        Tuple<Etiketa, int> editingEtiketa = null;

        ObservableCollection<Lokal> lokali = new ObservableCollection<Lokal>();
        ObservableCollection<Tip> tipovi = new ObservableCollection<Tip>();
        ObservableCollection<Etiketa> etikete = new ObservableCollection<Etiketa>();


        public ObservableCollection<Lokal> Lokali
        {
            get
            {
                return lokali;
            }

            set
            {
                lokali = value;
            }
        }

        public ObservableCollection<Tip> Tipovi
        {
            get
            {
                return tipovi;
            }

            set
            {
                tipovi = value;
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
            }
        }

        public Tuple<Lokal, int> EditingLokal
        {
            get
            {
                return editingLokal;
            }

            set
            {
                editingLokal = value;
            }
        }

        public Tuple<Tip, int> EditingTip
        {
            get
            {
                return editingTip;
            }

            set
            {
                editingTip = value;
            }
        }

        public Tuple<Etiketa, int> EditingEtiketa
        {
            get
            {
                return editingEtiketa;
            }

            set
            {
                editingEtiketa = value;
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //vrati u listu ukoliko je u toku editovanje
            if (editingLokal != null)
            {
                Lokali.Insert(editingLokal.Item2, editingLokal.Item1);
            }
            if (editingTip != null)
            {
                Tipovi.Insert(editingTip.Item2, editingTip.Item1);
            }
            if (editingEtiketa != null)
            {
                Etikete.Insert(editingEtiketa.Item2, editingEtiketa.Item1);
            }

            //snimi lokale
            string lokaliFile = "lokali.bin";
            using (Stream stream = File.Open(lokaliFile, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, lokali);
            }

            //snimi tipove
            string tipoviFile = "tipovi.bin";
            using (Stream stream = File.Open(tipoviFile, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, tipovi);
            }

            //snimi etikete
            string etiketeFile = "etikete.bin";
            using (Stream stream = File.Open(etiketeFile, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, etikete);
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //ucitaj tipove
            string tipoviFile = "tipovi.bin";
            if (File.Exists(tipoviFile))
            {
                using (Stream stream = File.Open(tipoviFile, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();
                    tipovi = (ObservableCollection<Tip>)bformatter.Deserialize(stream);
                }
            }

            ////ucitaj etikete
            string etiketeFile = "etikete.bin";
            if (File.Exists(etiketeFile))
            {
                using (Stream stream = File.Open(etiketeFile, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();
                    etikete = (ObservableCollection<Etiketa>)bformatter.Deserialize(stream);
                }
            }

            ////ucitaj lokale
            string lokaliFile = "lokali.bin";
            if (File.Exists(lokaliFile))
            {
                using (Stream stream = File.Open(lokaliFile, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();
                    lokali = (ObservableCollection<Lokal>)bformatter.Deserialize(stream);
                }
            }


            foreach (Lokal lokal in Lokali)
            {
                foreach (Tip tip in Tipovi)
                {
                    if (lokal.Tip.Oznaka.Equals(tip.Oznaka))
                    {
                        lokal.Tip = tip;
                        break;
                    }
                }
            }
        

            foreach (Lokal lokal in Lokali)
            {
                foreach (Etiketa e1 in lokal.Etikete.ToArray())
                {
                    foreach (Etiketa e2 in Etikete)
                    {
                        if (e1.Oznaka.Equals(e2.Oznaka))
                        {
                            lokal.Etikete.Remove(e1);
                            lokal.Etikete.Add(e2);
                        }
}
                }
            }

            //foreach(Lokal lokal in ((App)Application.Current).Lokali)
            //{
            //    foreach(Etiketa et in lokal.Etikete.ToArray())
            //    {
            //        if (!((App)Application.Current).Etikete.Contains(et))
            //        {
            //            lokal.Etikete.Remove(et);
            //        }
            //    }
            //}
            //ucitaj tipove iz .txt fajla
            //string[] tiplines = File.ReadAllLines("tipovi.txt");
            //for (int i = 0; i < tiplines.Length; i++)
            //{
            //    Tip tip = new Tip();
            //    tip.Oznaka = "T" + (i + 1);
            //    tip.Ime = tiplines[i].Trim();
            //    tip.Opis = tip.Ime;

            //    Tipovi.Add(tip);
            //}

            //ucitaj lokale iz .txt fajla
            //string[] lokalLines = File.ReadAllLines("lokali.txt");
            //for (int i = 0; i < lokalLines.Length; i++)
            //{
            //    string[] lineParts = lokalLines[i].Split(',');
            //    string nazivtipa = lineParts[0];
            //    string ime = lineParts[1];
            //    string logosrc = lineParts[2];
            //    string datum = lineParts[3].TrimEnd();

            //    Lokal lokal = new Lokal();

            //    //oznaka
            //    lokal.OznakaTxt = "L" + (i + 1);
            //    //ime
            //    lokal.ImeTxt = ime;
            //    //opis
            //    lokal.OpisTxt = "";
            //    //tip
            //    foreach (Tip tip in Tipovi)
            //    {
            //        if (tip.Ime.Equals(nazivtipa))
            //        {
            //            lokal.Tip = tip;
            //            break;
            //        }
            //    }
            //    //ikona
            //    if (!logosrc.Equals("/images/default/prostor_za_logo_vase_firme.jpg"))
            //    {
            //        if (File.Exists(@"C:\Users\Marko\Documents\Visual Studio 2015\Projects\HCIZadatak\HCIZadatak\bin\Debug\" + lokal.ImeTxt + ".jpg"))
            //        {
            //            lokal.Ikona = new BitmapImage(new Uri(@"C:\Users\Marko\Documents\Visual Studio 2015\Projects\HCIZadatak\HCIZadatak\bin\Debug\" + lokal.ImeTxt + ".jpg"));
            //        }
            //    }

            //    lokali.Add(lokal);
            //}

            ////alkohol
            //foreach (Lokal lokal in Lokali)
            //{
            //    if (lokal.Tip.Equals(Tipovi.ElementAt(0)))
            //    {
            //        lokal.Alkohol = "Da";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(1)))
            //    {
            //        lokal.Alkohol = "Do23h";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(2)))
            //    {
            //        lokal.Alkohol = "Da";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(3)))
            //    {
            //        lokal.Alkohol = "Ne";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(4)))
            //    {
            //        lokal.Alkohol = "Ne";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(5)))
            //    {
            //        lokal.Alkohol = "Do23h";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(6)))
            //    {
            //        lokal.Alkohol = "Ne";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(7)))
            //    {
            //        lokal.Alkohol = "Do23h";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(8)))
            //    {
            //        lokal.Alkohol = "Do23h";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(9)))
            //    {
            //        lokal.Alkohol = "Da";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(10)))
            //    {
            //        lokal.Alkohol = "Ne";
            //    }
            //    else if (lokal.Tip.Equals(Tipovi.ElementAt(11)))
            //    {
            //        lokal.Alkohol = "Da";
            //    }
            //}

            ////hendikep,pusenje,rezervacije
            //for (int i = 0; i < lokali.Count; i++)
            //{
            //    int c = i % 4;

            //    lokali[i].Cena = Lokal.Rang_cena[c];

            //    if (i % 3 == 0)
            //    {
            //        lokali[i].Hendikep = true;
            //    }
            //    if (i % 2 == 0)
            //    {
            //        lokali[i].Pusenje = true;
            //    }
            //    if (i % 5 == 0)
            //    {
            //        lokali[i].Rezervacije = true;
            //    }

            //    Random rnd = new Random();
            //    lokali[i].Kapacitet = rnd.Next(1000);


        }
    }
}

