
using MauiScanApp.Models;
using MauiScanApp.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using System.Net.Http.Json;
using System.Text.Json;
using ZXing;

namespace MauiScanApp.Views;

public partial class PgAttendList : ContentPage
{
    CAttendViewModel model = new CAttendViewModel();
    public PgAttendList()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        App app = Application.Current as App;
        lvAttendList.ItemsSource = model.All;
    }

    private async void lvAttendList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        CAttendList a = e.CurrentSelection.FirstOrDefault() as CAttendList;
        int id = a.OrderDetailId;
        bool answer = await DisplayAlert("報到", a.Name, "Yes", "No");
        if (answer)
        {
            ActivityIndicator loader = new ActivityIndicator();
            loader.IsRunning = true;
            loader.IsVisible = true;
            stackLayout.Children.Add(loader);
            await Check(id,a);

            //int pdId = 0;
            //App app = Application.Current as App;
            //if (app.selectedProductDetailId != null)
            //    pdId = (int)app.selectedProductDetailId;

            //await Check(pdId, a.odNumber);
            loader.IsRunning = false;
            loader.IsVisible = false;
        }
    }

    //private async Task Check(int pdId, string qrCode)
    //{
    //    using (HttpClient client = new HttpClient())
    //    {
    //        Uri uri = new Uri($"http://10.0.2.2:7208/order/scanQrCode?productDetailId={pdId}&qrcode={qrCode}");
    //        HttpResponseMessage message = client.GetAsync(uri).Result;
    //        if (message.IsSuccessStatusCode)
    //        {
    //            string s = message.Content.ReadAsStringAsync().Result;
    //            DisplayAlert("Alert", s, "OK");
    //        }
    //    }
    //}

    private async Task Check(int id, CAttendList attend)
    {
        //將OrderDetail put 回去
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://10.0.2.2:5016/");
            HttpResponseMessage message =
            await client.PutAsJsonAsync(string.Format("/api/Attend/{0}", id), attend);
            message.EnsureSuccessStatusCode();
            if (message.IsSuccessStatusCode)
            {
                DisplayAlert("Alert", "報到成功", "OK");
            }
            else
            {
                DisplayAlert("Alert", message.StatusCode.ToString(), "OK");
            }
        }

        //重整報到畫面
        int? productDetailId = null;
        App app = Application.Current as App;
        if (app.selectedProductDetailId != null)
            productDetailId = (int)app.selectedProductDetailId;
        using (HttpClient client = new HttpClient())
        {
            Uri uri = new Uri($"http://10.0.2.2:5016/api/OrderDetails/AttendList?id={productDetailId}");
            app.attendLists = await client.GetFromJsonAsync<List<CAttendList>>(uri);
            model.All = app.attendLists;
            lvAttendList.ItemsSource = app.attendLists;
        };
    }
}