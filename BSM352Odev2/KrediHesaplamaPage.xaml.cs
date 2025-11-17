using System.Diagnostics;
using System.Globalization;

namespace BSM352Odev2;

public partial class KrediHesaplamaPage : ContentPage
{
    public KrediHesaplamaPage()
    {
        InitializeComponent();
        PickerKrediTuru.SelectedIndex = 0;
    }

    private void SliderVade_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider'ý deðiþtikçe vadeyi deðiþtirir.
        int vade = (int)e.NewValue;
        LblVade.Text = $"{vade} Ay";
    }

    private void BtnHesapla_Clicked(object sender, EventArgs e)
    {
        // Valýdasyon iþlemini yapar. Biz boþ býraktýðýmýzda veya sayý yerine baþka bir þey girmemizi engeller. 

        
        // uygulamanýn çökmesini engeller.
        string tutarStr = EntryTutar.Text?.Replace(",", ".") ?? string.Empty;
        string faizStr = EntryFaiz.Text?.Replace(",", ".") ?? string.Empty;

        if (!double.TryParse(tutarStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double tutar) || tutar <= 0)
        {
            DisplayAlert("Hata", "Lütfen geçerli bir kredi tutarý girin.", "Tamam");
            return;
        }

        if (!double.TryParse(faizStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double aylikFaizOrani) || aylikFaizOrani <= 0)
        {
            DisplayAlert("Hata", "Lütfen geçerli bir aylýk faiz oraný girin.", "Tamam");
            return;
        }


        if (PickerKrediTuru.SelectedIndex == -1)
        {
            DisplayAlert("Hata", "Lütfen bir kredi türü seçin.", "Tamam");
            return;
        }


        int vade = (int)SliderVade.Value;
        string krediTuru = PickerKrediTuru.SelectedItem.ToString();


        double bsmv = 0;    //Banka ve Siorta Muammeleleri Vergisi. Kredi türünüe göre deðiþkenlik gösteriri. 
        double kkdf = 0;    // Kaynak Kullanýmý Destekleme Fonu. Kredi türünüe göre deðiþkenlik gösteriri. 

        switch (krediTuru)
        {
            case "Ýhtiyaç Kredisi":
                bsmv = 0.10;
                kkdf = 0.15;
                break;
            case "Taþýt Kredisi":
                bsmv = 0.05;
                kkdf = 0.15;
                break;
            case "Konut Kredisi":
                bsmv = 0;
                kkdf = 0;
                break;
            case "Ticari Kredisi":
                bsmv = 0.05;
                kkdf = 0;
                break;
        }


        double oran = aylikFaizOrani / 100.0;
        double brutFaiz = oran + (oran * bsmv) + (oran * kkdf);

        double taksit;

        if (brutFaiz == 0)
        {
            taksit = tutar / vade;
        }
        else
        {

            taksit = (tutar * brutFaiz * Math.Pow(1 + brutFaiz, vade)) / (Math.Pow(1 + brutFaiz, vade) - 1);
        }

        double toplamOdeme = taksit * vade;
        double toplamFaiz = toplamOdeme - tutar;

        // Yukarýdaki hesaplamalar yapýldýktan sonra aþaðý kodda sonuclar verir.

        LblAylikTaksit.Text = $"Aylýk Taksit: {taksit:F2} TL";
        LblToplamOdeme.Text = $"Toplam Ödeme: {toplamOdeme:F2} TL";
        LblToplamFaiz.Text = $"Toplam Faiz: {toplamFaiz:F2} TL";
    }
}