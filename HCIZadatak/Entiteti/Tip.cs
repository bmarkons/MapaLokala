using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HCIZadatak.Entiteti
{
    [Serializable]
    public class Tip : INotifyPropertyChanged
    {
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
        private string oznaka;
        private string ime;
        [NonSerialized]
        private BitmapImage ikona;
        private string opis;

        public string Oznaka
        {
            get
            {
                return oznaka;
            }

            set
            {
                oznaka = value;
                OnPropertyChanged("Oznaka");
            }
        }

        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }

        public BitmapImage Ikona
        {
            get
            {
                return ikona;
            }

            set
            {
                ikona = value;
                OnPropertyChanged("Ikona");
            }
        }

        public string Opis
        {
            get
            {
                return opis;
            }

            set
            {
                opis = value;
                OnPropertyChanged("Opis");
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

        public override string ToString()
        {
            return "[" + oznaka + "] " + ime;
        }
    }
}
