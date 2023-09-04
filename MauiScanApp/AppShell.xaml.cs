namespace MauiScanApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Views.PgStatus), typeof(Views.PgStatus));
    }
}
