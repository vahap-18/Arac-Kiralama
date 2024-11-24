using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arac_Kiralama
{
    internal class Araba
    {
        private string _plaka;

        public string Plaka
        {
            get => _plaka;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Plaka boş olamaz.");
                _plaka = PlakaDogrula(value);
            }
        }

        public string Marka { get; set; }
        public int KiralamaBedeli { get; set; }
        public string AracTipi { get; set; }
        public string Durum { get; set; } = "Galeride";
        public int KiralamaSuresi { get; set; }
        public List<int> KiralamaSureleri { get; set; } = new List<int>();
        public void KiralamaEkle(int sure)
        {
            KiralamaSureleri.Add(sure);
        }
        public int ToplamKiralamaSureleri => KiralamaSureleri.Sum();

        public Araba(string plaka, string marka, int kiralamaBedeli, string aracTipi)
        {
            Plaka = plaka;
            Marka = marka ?? throw new ArgumentNullException(nameof(marka));
            KiralamaBedeli = kiralamaBedeli > 0 ? kiralamaBedeli : throw new ArgumentException("Kiralama bedeli sıfırdan büyük olmalıdır.");
            AracTipi = aracTipi ?? throw new ArgumentNullException(nameof(aracTipi));
        }

        public Araba()
        {

        }

        private string PlakaDogrula(string giris)
        {
            string pattern = @"^\d{1,2}[A-Z]{1,3}\d{2,4}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(giris.ToUpper(), pattern))
                throw new ArgumentException("Geçersiz plaka formatı.");
            return giris.ToUpper();
        }

        public override string ToString()
        {
            return $"{Plaka} - {Marka} ({KiralamaBedeli} TL/saat) - {AracTipi} - {Durum}";
        }
    }
}
