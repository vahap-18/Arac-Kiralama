# Araba Kiralama Otomasyonu

Bu proje, bir araba kiralama galerisinin temel işlemlerini yönetmek için geliştirilmiş bir konsol uygulamasıdır. Program, araç kiralama, teslim alma, galeri bilgilerini görüntüleme gibi işlevler sunar ve kullanıcı dostu bir menü üzerinden kontrol edilir.

## Özellikler

1. **Araba Kiralama**: Plaka ve süre belirterek araba kiralayabilirsiniz.
2. **Araba Teslim Alma**: Kiralanan aracı galeriye geri alabilirsiniz.
3. **Arabaları Listeleme**:
   - Kirada olan araçları listeleyin.
   - Galeride bekleyen araçları listeleyin.
   - Tüm araçları görüntüleyin.
4. **Araba Ekleme ve Silme**:
   - Yeni bir araç ekleyebilirsiniz.
   - Galeriden araç silebilirsiniz.
5. **Genel Bilgileri Görüntüleme**: Toplam araç sayısı, ciro, kiralama süreleri gibi genel istatistikleri görüntüleyebilirsiniz.

## Nasıl Çalıştırılır?

1. **Projeyi klonlayın**:
   ```bash
   git clone <repo-link>
   cd ArabaKiralamaOtomasyonu
   ```

2. **Projeyi Visual Studio veya bir C# IDE'sinde açın**.

3. **Çalıştırın**:
   Uygulama başlatıldığında bir ana menü görüntülenir.

4. **Ana Menüyü Kullanma**:
   Menüden işleminizi seçmek için sayı veya harf kombinasyonlarını kullanın. Örneğin:
   - `1` veya `K`: Araba kiralama
   - `2` veya `T`: Araba teslim alma

## Örnek Kullanımlar

### Araba Kiralama
```plaintext
-Araba Kirala-

Kiralanacak arabanın plakası: 34TT2305
Kiralama süresi (saat): 5

34TT2305 plakalı araba 5 saatliğine kiralandı.
```

### Kiradaki Arabaları Listeleme
```plaintext
-Kiradaki Arabalar-

Plaka      Marka       K. Bedeli      Araba Tipi      Durum
-----------------------------------------------------------
34TT2305   Opel        50             SUV             Kirada
36MN2304   Fiat        150            Hatchback       Kirada
```

### Galeri Bilgileri Görüntüleme
```plaintext
-Genel Bilgiler-

Toplam Araba Sayısı: 10
Kiradaki Araba Sayısı: 3
Bekleyen Araba Sayısı: 7
Toplam Kiralama Süresi: 42 saat
Toplam Araba Kiralama Adedi: 15
Ciro: 6300 TL
```

## Geliştiriciler İçin

### Kod Yapısı

- **`Program.cs`**: Uygulamanın ana mantığını içerir. Menü ve işlemler burada tanımlanmıştır.
- **`Galeri.cs`**: Araçların galeri içindeki durumlarını ve işlevlerini yönetir.
- **`Araba.cs`**: Her bir araba için model yapısını tanımlar.

### Veri Doğrulama

- **Plaka Doğrulama**: Türk plaka formatına uygunluk kontrolü için regex kullanılır.
  ```csharp
  string pattern = @"^\d{1,2}[A-Z]{1,3}\d{2,4}$";
  Regex.IsMatch(plaka, pattern);
  ```

### Sahte Veriler

Program başlangıcında otomatik olarak şu araçlar eklenir:
- **34TT2305** - Opel, SUV
- **36MN2304** - Fiat, Hatchback

Bu araçlarla test işlemleri yapabilirsiniz.



**Not:** Uygulama kullanıcıdan gelen girişleri hassas şekilde kontrol eder ve 10 hatalı giriş sonrası program güvenlik gerekçesiyle kapanır.
