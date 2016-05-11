using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCIZadatak.Validation
{
    public class OznakaLokalaValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                string s = value as string;
                ObservableCollection<Lokal> lokali = ((App)App.Current).Lokali;
                foreach (Lokal lokal in lokali)
                {
                    if (s.Equals(lokal.OznakaTxt))
                    {
                        return new ValidationResult(false, "Oznaka već postoji");
                    }
                }

                if (s.Equals(""))
                {
                    return new ValidationResult(false, "Oznaka mora biti unešena");
                }
                else
                {
                    return new ValidationResult(true, null);
                }

            }
            catch
            {
                return new ValidationResult(false, "Greška");
            }
        }
    }
}
