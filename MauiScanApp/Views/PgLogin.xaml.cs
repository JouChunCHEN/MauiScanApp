using System.Net.Http.Json;

namespace MauiScanApp.Views;

public partial class PgLogin : ContentPage
{
	public PgLogin()
	{
		InitializeComponent();
	}

    private void txtTaxId_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        Uri uri = new Uri($"http://10.0.2.2:5016/api/Suppliers/CheckSupplier?taxId={txtTaxId.Text}");
        CLoginInfo x = await client.GetFromJsonAsync<CLoginInfo>(uri);

        if (x != null)
        {
            if(x.Password == txtPassword.Text)
            {
                DisplayAlert("Alert", "µn¤J¦¨¥\", "OK");
            }
        }
    }
}