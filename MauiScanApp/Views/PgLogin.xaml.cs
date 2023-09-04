using System.Net.Http.Json;

namespace MauiScanApp.Views;

public partial class PgLogin : ContentPage
{
    private bool isTextChanged = true;
    private string tempTaxId = "";
    private CLoginInfo x = null;
    public PgLogin()
	{
		InitializeComponent();
	}

    private void txtTaxId_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(!string.IsNullOrEmpty(tempTaxId) &&  tempTaxId != txtTaxId.Text)
            isTextChanged = !isTextChanged;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (isTextChanged)
        {
            tempTaxId = txtTaxId.Text;
            ActivityIndicator loader = new ActivityIndicator();
            loader.IsRunning = true;
            loader.IsVisible = true;
            stackLayout.Children.Add(loader);
            HttpClient client = new HttpClient();
            Uri uri = new Uri($"http://10.0.2.2:5016/api/Suppliers/CheckSupplier?taxId={txtTaxId.Text}");
            x = await client.GetFromJsonAsync<CLoginInfo>(uri);

            loader.IsRunning = false;
            loader.IsVisible = false;
            if (x != null)
            {
                if (x.Password == txtPassword.Text)
                {
                    await DisplayAlert("歡迎", "登入成功", "OK");
                    App app = Application.Current as App;
                    app.loggedSupplierId = x.SupplierId;
                    app.MainPage = new AppShell();
                }
                else
                {
                    DisplayAlert("錯誤", "輸入帳密有誤，請重新輸入", "OK");
                }
            }
        }
        else
        {
            if (x.Password == txtPassword.Text)
            {
                await DisplayAlert("Alert", "登入成功", "OK");
                App app = Application.Current as App;
                app.loggedSupplierId = x.SupplierId;
                app.MainPage = new AppShell();
            }
            else
            {
                DisplayAlert("提示", "輸入帳密有誤，請重新輸入", "OK");
            }
        }

    }
}