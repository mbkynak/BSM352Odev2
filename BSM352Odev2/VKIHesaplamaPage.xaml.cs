namespace BSM352Odev2;

public partial class VkiHesaplamaPage : ContentPage
{
    public VkiHesaplamaPage()
    {
        InitializeComponent();
        //Hesaplama yapar ve F1 virgülden sonra bir basamak almasý içindir.
        EntryKilo.Text = SliderKilo.Value.ToString("F1");
        EntryBoy.Text = SliderBoy.Value.ToString("F1");
        HesaplaVeGoster();
    }

    
    private void SliderKilo_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Sliderin durumuna göre Entry kutusunu günceller.
        EntryKilo.Text = e.NewValue.ToString("F1");
        HesaplaVeGoster();
    }

     
    private void SliderBoy_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Sliderin durumuna göre Entry kutusunu günceller.
        EntryBoy.Text = e.NewValue.ToString("F1");
        HesaplaVeGoster();
    }

    
    private void EntryKilo_Completed(object sender, EventArgs e)
    {
        //Girilen deðerin geçerli olup olmadýðýna bakar.
        if (double.TryParse(EntryKilo.Text, out double kiloValue))
        {
            SliderKilo.Value = kiloValue;
        }
        else
        {
            
            EntryKilo.Text = SliderKilo.Value.ToString("F1");
        }
    }

    private void EntryBoy_Completed(object sender, EventArgs e)
    {
        
        if (double.TryParse(EntryBoy.Text, out double boyValue))
        {           
            SliderBoy.Value = boyValue;
        }
        else
        {
            EntryBoy.Text = SliderBoy.Value.ToString("F1");
        }
    }

   //Deðerlere göre hesaplama iþlemi aþaðýdaki kodalrda yapýlýr.
    private void HesaplaVeGoster()
    {
        
        double kilo = SliderKilo.Value;
        double boyCm = SliderBoy.Value;
        double boyMetre = boyCm / 100.0;

        if (boyMetre == 0) return;

        
        double vki = kilo / (boyMetre * boyMetre);

        // Sonuçlarý gösterir.
        LblVkiSonuc.Text = vki.ToString("F2");
        LblVkiKategori.Text = GetVkiKategorisi(vki);
    }

    private string GetVkiKategorisi(double vki)
    {
        if (vki < 16) 
            return "Ýleri Düzeyde Zayýf";

        if (vki < 17)
            return "Orta Düzeyde Zayýf";

        if (vki < 18.5) 
            return "Hafif Düzeyde Zayýf";

        if (vki < 25) 
            return "Normal Kilolu";

        if (vki < 30) 
            return "Hafif Þiþman / Fazla Kilolu";

        if (vki < 35) 
            return "1. Derecede Obez";

        if (vki < 40) 
            return "2. Derecede Obez";

        return "3. Derecede Obez / Morbid Obez";
    }
}