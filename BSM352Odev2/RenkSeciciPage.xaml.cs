namespace BSM352Odev2;

public partial class RenkSeciciPage : ContentPage
{
    // Random tusuna basýnca rastgele bir sayý veriyor.
    private Random random = new Random();

    public RenkSeciciPage()
    {
        InitializeComponent();
        UpdateColor();
    }

    // Herhangi bir slider deðiþtiðinde bu metod çalýþýr
    private void OnColorChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        // 0 ile 255 arasý deger alýr slider.
        int red = (int)SliderRed.Value;
        int green = (int)SliderGreen.Value;
        int blue = (int)SliderBlue.Value;

        // Etikette gösterir.
        LblRed.Text = red.ToString();
        LblGreen.Text = green.ToString();
        LblBlue.Text = blue.ToString();

        
        Color secilenRenk = Color.FromRgb(red, green, blue);   // rengi seçilen renge aktarýr.

        
        string hexKodu = secilenRenk.ToHex();   // hex kodunu oluþtur ve göster
        LblHexKodu.Text = hexKodu;

        //seçilen rengi arka planda güncellememizi saðlar.
        LblHexKodu.TextColor = secilenRenk;
    }

    private async void BtnKopyala_Clicked(object sender, EventArgs e)
    {
        //kopyalama 
        string renk_kodu = LblHexKodu.Text;
        await Clipboard.SetTextAsync(renk_kodu);
        await DisplayAlert("Kopyalandý", $"{renk_kodu} panoya kopyalandý.", "OK");
    }

    private void BtnRastgele_Clicked(object sender, EventArgs e)
    {
        //Rastgele renkler seçer
        SliderRed.Value = random.Next(256);
        SliderGreen.Value = random.Next(256);
        SliderBlue.Value = random.Next(256);
    }
}