using MauiScanApp.Views;

namespace MauiScanApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Views.PgStatus), typeof(Views.PgStatus));
        Routing.RegisterRoute("PgLogin", typeof(Views.PgLogin));
    }


    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("登出", "確定要登出嗎？", "Yes", "No");
        if (answer)
        {
            App app = Application.Current as App;
            app.loggedSupplierId = null;
            app.MainPage = new PgLogin();
        }
    }
}
