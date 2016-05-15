using HCIZadatak.Entiteti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HCIZadatak.Validation
{
    class OznakaEtiketeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                string s = value as string;
                ObservableCollection<Etiketa> etikete = ((App)App.Current).Etikete;
                foreach (Etiketa etiketa in etikete)
                {
                    if (s.Equals(etiketa.Oznaka))
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
