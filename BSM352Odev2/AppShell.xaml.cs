namespace BSM352Odev2;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

      
        Routing.RegisterRoute(nameof(KrediHesaplamaPage), typeof(KrediHesaplamaPage));
        Routing.RegisterRoute(nameof(VkiHesaplamaPage), typeof(VkiHesaplamaPage));
        Routing.RegisterRoute(nameof(RenkSeciciPage), typeof(RenkSeciciPage));
    }
}