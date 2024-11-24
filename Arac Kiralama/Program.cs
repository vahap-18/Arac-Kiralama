
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Arac_Kiralama
{
    internal class Program
    {
        static Galeri OtoGaleri = new Galeri();
        static Araba araba = new Araba();
        static int Hatalar = 0;

        static void Main(string[] args)
        {
            SahteArabaEkle();
            AnaMenu();
            while (true)
            {
                if (Hatalar >= 10)
                {
                    Console.WriteLine("Çok fazla yanlış giriş yaptınız. Program sonlandırılıyor.");
                    break;
                }

                Console.Write("\nSeçiminiz : ");
                string secim = Console.ReadLine().ToUpper();
                Cikis(secim);
                Console.WriteLine();

                if (secim == "X") continue;

                switch (secim)
                {
                    case "1":
                    case "K":
                        ArabaKirala();
                        break;
                    case "2":
                    case "T":
                        ArabaTeslimAl();
                        break;
                    case "3":
                    case "R":
                        KiradakiArabalar();
                        break;
                    case "4":
                    case "M":
                        GaleridekiArabalar();
                        break;
                    case "5":
                    case "A":
                        TumArabalar();
                        break;
                    case "6":
                    case "I":
                        KiralamaIptali();
                        break;
                    case "7":
                    case "Y":
                        ArabaEkle();
                        break;
                    case "8":
                    case "S":
                        ArabaSil();
                        break;
                    case "9":
                    case "G":
                        GaleriBilgileri();
                        break;
                    default:
                        Hatalar++;
                        Console.WriteLine($"Hatalı işlem gerçekleştirildi. {10 - Hatalar} hakkınız kaldı.");
                        break;
                }
            }
        }

        static void AnaMenu()
        {
            Console.WriteLine("\nGaleri Otomasyon");
            Console.WriteLine("1- Araba Kirala (K)");
            Console.WriteLine("2- Araba Teslim Al (T)");
            Console.WriteLine("3- Kiradaki Arabaları Listele (R)");
            Console.WriteLine("4- Galerideki Arabaları Listele (M)");
            Console.WriteLine("5- Tüm Arabaları Listele (A)");
            Console.WriteLine("6- Kiralama İptali (I)");
            Console.WriteLine("7- Araba Ekle (Y)");
            Console.WriteLine("8- Araba Sil (S)");
            Console.WriteLine("9- Bilgileri Göster (G)");
            Console.Write("");
        }

        static void ArabaKirala()
        {
            Console.WriteLine("-Araba Kirala-\n");
            string plaka;

            do
            {
                Console.Write("Kiralanacak arabanın plakası: ");
                plaka = Console.ReadLine().ToUpper();
                Cikis(plaka);

                if (!PlakaGecerliMi(plaka))
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Lütfen tekrar deneyin.");
                }
            } while (!PlakaGecerliMi(plaka));


            var araba = OtoGaleri.ArabaBul(plaka);
            if (araba == null)
            {
                Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                return;
            }

            Console.Write("Kiralama süresi (saat): ");
            if (int.TryParse(Console.ReadLine(), out int sure) && sure > 0)
            {
                Cikis(sure.ToString());

                if (araba.Durum != "Kirada")
                {
                    araba.Durum = "Kirada";
                    araba.KiralamaSureleri.Add(sure);

                    Console.WriteLine($"{plaka} plakalı araba {sure} saatliğine kiralandı.");
                }
                else
                {
                    Console.WriteLine("Araba şu an kirada. Farklı bir araç seçiniz.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz süre girişi. Tekrar deneyin.");
            }
        }


        public static void ArabaTeslimAl()
        {
            Console.WriteLine("-Araba Teslim Al-\n");
            string plaka;

            do
            {
                Console.Write("Teslim edilecek arabanın plakası: ");
                plaka = Console.ReadLine().ToUpper();
                Cikis(plaka);
                if (!PlakaGecerliMi(plaka))
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                }
            } while (!PlakaGecerliMi(plaka));


            var araba = OtoGaleri.ArabaBul(plaka);
            if (araba != null && araba.Durum == "Galeride")
            {
                Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
            }
            else if (araba != null && araba.Durum == "Kirada")
            {
                OtoGaleri.ArabaTeslimAl(plaka);
                Console.WriteLine("Araba galeride beklemeye alındı.");
            }
            else
            {
                Console.WriteLine("Galeriye ait bu plakada araba yoktur.");
            }
        }


        static void KiradakiArabalar()
        {
            Console.WriteLine("-Kiradaki Arabalar-\n");
            ArabalariListele("Kirada");
        }


        static void GaleridekiArabalar()
        {

            Console.WriteLine("-Galerideki Arabalar-\n");
            ArabalariListele("Galeride");
        }

        static void TumArabalar()
        {
            Console.WriteLine("-Tüm Arabalar-\n");
            ArabalariListele();
        }

        static void ArabalariListele(string durum = null)
        {
            var arabalar = OtoGaleri.Listele(durum);
            if (arabalar.Any())
            {
                Console.WriteLine("Plaka".PadRight(11) + "Marka".PadRight(13) + "K. Bedeli".PadRight(15) + "Araba Tipi".PadRight(15) + "Durum");
                Console.WriteLine("----------------------------------------------------------------");
                foreach (var araba in arabalar)
                {
                    Console.WriteLine($"{araba.Plaka.PadRight(10)} {araba.Marka.PadRight(10)} {Convert.ToString(araba.KiralamaBedeli).PadRight(15)} {araba.AracTipi.PadRight(15)} {araba.Durum}");
                }
            }
            else
            {
                Console.WriteLine("Listelenecek araba bulunamadı.");
            }
        }

        public static void KiralamaIptali()
        {
            Console.WriteLine("-Kiralama İptali-\n");
            string plaka;
            do
            {
                Console.Write("Kiralama iptal edilecek arabanın plakası: ");
                plaka = Console.ReadLine().ToUpper();
                Cikis(plaka);
                if (!PlakaGecerliMi(plaka))
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Lütfen tekrar deneyin.");
                }
            } while (!PlakaGecerliMi(plaka));

            var araba = OtoGaleri.ArabaBul(plaka.ToUpper());

            if (araba != null && araba.Durum == "Galeride")
            {
                Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
            }

            else if (araba != null && araba.Durum == "Kirada")
            {
                OtoGaleri.ArabaTeslimAl(plaka);

                int toplamSüre = araba.KiralamaSureleri.Sum();

                Console.WriteLine("İptal gerçekleştirildi.");

            }
            else
            {
                Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
            }
        }


        static void ArabaEkle()
        {
            Console.WriteLine("-Araba Ekle-\n");
            string plaka;
            do
            {
                Console.Write("Plaka: ");
                plaka = Console.ReadLine().ToUpper();
                Cikis(plaka);

                if (!PlakaGecerliMi(plaka))
                {
                    Console.Write("Bu şekilde plaka girişi yapamazsınız. Lütfen tekrar deneyin : ");
                }
            } while (!PlakaGecerliMi(plaka));

            Console.Write("Marka: ");
            string marka;
            do
            {
                Console.Write("Marka: ");
                marka = Console.ReadLine();
                if (int.TryParse(marka, out _) || double.TryParse(marka, out _))
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                }
            } while (int.TryParse(marka, out _) || double.TryParse(marka, out _));

            Cikis(marka);

            int kiralamaBedeli;
            do
            {
                Console.Write("Kiralama Bedeli: ");

                if (!int.TryParse(Console.ReadLine(), out kiralamaBedeli) || kiralamaBedeli <= 0)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                }
            } while (kiralamaBedeli <= 0);

            Console.WriteLine("Araba Tipi: ");
            Console.WriteLine("SUV için 1");
            Console.WriteLine("Hatchback için 2");
            Console.WriteLine("Sedan için 3");

            string[] tipler = { "SUV", "Hatchback", "Sedan" };

            Console.WriteLine("Araba Tipi (1: SUV, 2: Hatchback, 3: Sedan): ");
            if (int.TryParse(Console.ReadLine(), out int secim) && secim >= 1 && secim <= 3)
            {
                if (OtoGaleri.ArabaEkle(plaka.ToUpper(), marka, kiralamaBedeli, tipler[secim - 1]))
                    Console.WriteLine("Araba başarıyla eklendi.");
                else
                    Console.WriteLine("Aynı plakada araba mevcut. Girdiğiniz plakayı kontrol edin.");
            }
            else
            {
                Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
            }

        }


        static void ArabaSil()
        {
            Console.WriteLine("-Araba Sil-\n");
            string plaka;
            do
            {
                Console.Write("Silinecek arabanın plakası: ");
                plaka = Console.ReadLine();
                Cikis(plaka);

                if (!PlakaGecerliMi(plaka))
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Lütfen tekrar deneyin.");
                }
            } while (!PlakaGecerliMi(plaka));

            if (araba == null)
            {
                Console.WriteLine("Galeriye ait bu plakada araba yok.");
            }
            if (OtoGaleri.ArabaSil(plaka.ToUpper()))
                Console.WriteLine("Araba başarıyla silindi.");
            else
                Console.WriteLine("Araba kirada olduğu için silme işlemi gerçekleştirilemedi.");
        }


        static void GaleriBilgileri()
        {
            Console.WriteLine("-Genel Bilgiler- \n");
            Console.WriteLine($"Toplam Araba Sayısı: {OtoGaleri.ToplamArabaSayisi}");
            Console.WriteLine($"Kiradaki Araba Sayısı: {OtoGaleri.KiradakiArabaSayisi}");
            Console.WriteLine($"Bekeyen Araba Sayısı: {OtoGaleri.GaleridekiArabaSayisi}");
            Console.WriteLine($"Toplam Kiralama Süresi: {OtoGaleri.ToplamArabaKiralamaSureleri} saat");
            Console.WriteLine($"Toplam Araba Kiralama Adedi: {OtoGaleri.ToplamArabaKiralamaAdeti}");
            Console.WriteLine($"Ciro: {OtoGaleri.Ciro} TL");
        }

        private static bool PlakaGecerliMi(string plaka)
        {
            if (string.IsNullOrWhiteSpace(plaka))
                return false;

            plaka = plaka.ToUpper();

            string pattern = @"^\d{1,2}[A-Z]{1,3}\d{2,4}$";
            return Regex.IsMatch(plaka, pattern);
        }

        static void SahteArabaEkle()
        {
            OtoGaleri.ArabaEkle("34TT2305", "Opel", 50, "SUV");
            OtoGaleri.ArabaEkle("36MN2304", "Fiat", 150, "Hatchback");
            OtoGaleri.ArabaEkle("34KSL525", "Audi", 170, "SUV");
            OtoGaleri.ArabaEkle("02ABN2541", "Fiat", 250, "Sedan");
        }

        static void Cikis(string input)
        {
            if (input.Equals("x", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Program sonlandırılıyor...");
                Environment.Exit(0);
            }
        }
    }
}