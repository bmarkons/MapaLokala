using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.Serialization;


namespace HCIZadatak.Entiteti
{
    [Serializable]
    public class Etiketa : INotifyPropertyChanged
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
        [NonSerialized]
        private Color boja;
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

        public Color Boja
        {
            get
            {
                return boja;
            }

            set
            {
                boja = value;
                OnPropertyChanged("Boja");
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
        private float a, r, g, b;
        [OnSerializing]
        private void DecomposeColor(StreamingContext context)
        {
            a = boja.ScA;
            r = boja.ScR;
            g = boja.ScG;
            b = boja.ScB;
        }

        [OnDeserialized]
        private void ComposeColor(StreamingContext context)
        {
            boja = Color.FromScRgb(a, r, g, b);
        }
        #endregion

        public override string ToString()
        {
            return this.Oznaka;
        }

        public Etiketa Clone()
        {
            Etiketa etiketa = new Etiketa();

            etiketa.Oznaka = Oznaka;
            etiketa.Opis = Opis;
            etiketa.Boja = Boja;
            etiketa.a = a;
            etiketa.b = b;
            etiketa.r = r;
            etiketa.g = g;

            return etiketa;
        }

    }
}
