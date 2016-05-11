using HCIZadatak.Entiteti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;

namespace HCIZadatak
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
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

        private void Application_Exit(object sender, ExitEventArgs e)
        {
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
            //ucitaj lokale
            string lokaliFile = "lokali.bin";
            if (File.Exists(lokaliFile))
            {
                using (Stream stream = File.Open(lokaliFile, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();
                    lokali = (ObservableCollection<Lokal>)bformatter.Deserialize(stream);
                }
            }

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

            //ucitaj etikete
            string etiketeFile = "etikete.bin";
            if (File.Exists(etiketeFile))
            {
                using (Stream stream = File.Open(etiketeFile, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();
                    etikete = (ObservableCollection<Etiketa>)bformatter.Deserialize(stream);
                }
            }
        }
    }
}
