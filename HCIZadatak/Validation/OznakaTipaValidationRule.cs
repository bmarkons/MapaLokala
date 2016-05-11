using HCIZadatak.Entiteti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCIZadatak.Validation
{
    class OznakaTipaValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                string s = value as string;
                ObservableCollection<Tip> tipovi = ((App)App.Current).Tipovi;
                foreach (Tip tip in tipovi)
                {
                    if (s.Equals(tip.Oznaka))
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
