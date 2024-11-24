using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arac_Kiralama
{
    internal class Galeri
    {
        private List<Araba> arabalar = new List<Araba>();

        public Araba ArabaBul(string plaka)
        {
            return arabalar.FirstOrDefault(a => a.Plaka == plaka);
        }
        public int ToplamArabaSayisi => arabalar.Count;

        public int KiradakiArabaSayisi => arabalar.Count(a => a.Durum == "Kirada");

        public int GaleridekiArabaSayisi => arabalar.Count(a => a.Durum == "Galeride");

        public int ToplamArabaKiralamaAdeti => arabalar.Sum(a => a.KiralamaSureleri.Count);

        public int ToplamArabaKiralamaSureleri => arabalar.Sum(a => a.ToplamKiralamaSureleri);

        public float Ciro => arabalar.Sum(a => a.ToplamKiralamaSureleri * a.KiralamaBedeli);

        public bool ArabaEkle(string plaka, string marka, int kiralamaBedeli, string aracTipi)
        {
            if (arabalar.Any(a => a.Plaka == plaka))
                return false;

            var yeniAraba = new Araba(plaka, marka, kiralamaBedeli, aracTipi);
            arabalar.Add(yeniAraba);
            return true;
        }

        public bool ArabaSil(string plaka)
        {
            var araba = arabalar.FirstOrDefault(a => a.Plaka == plaka);
            if (araba == null || araba.Durum == "Kirada")
                return false;

            arabalar.Remove(araba);
            return true;
        }

        public void ArabaKirala(string plaka, int sure)
        {

            var araba = arabalar.FirstOrDefault(a => a.Plaka == plaka);

            if (araba != null && araba.Durum == "Galeride")
            {
                araba.Durum = "Kirada";
                araba.KiralamaSureleri.Add(sure);
            }
            else
            {
                Console.WriteLine("Kiralanmak istenen araba bulunamadı veya zaten kirada.");
            }
        }

        public void ArabaTeslimAl(string plaka)
        {
            var araba = arabalar.FirstOrDefault(a => a.Plaka == plaka);

            if (araba != null && araba.Durum == "Kirada")
            {
                araba.Durum = "Galeride";
            }
            else
            {
                Console.WriteLine("Teslim alınacak araba bulunamadı veya zaten galeride.");
            }
        }
        public List<Araba> Listele(string durum = null)
        {
            return durum == null ? arabalar : arabalar.Where(a => a.Durum == durum).ToList();
        }
    }
}
