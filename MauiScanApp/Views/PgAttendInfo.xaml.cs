using MauiScanApp.Models;
using MauiScanApp.ViewModels;
using System.Net.Http.Json;

namespace MauiScanApp.Views;

public partial class PgAttendInfo : ContentPage
{
    CAttendViewModel model = new CAttendViewModel();
	public PgAttendInfo()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        App app = Application.Current as App;
        if (app != null)
        {
            if (app.attendLists == null)
            {
                loader.IsRunning = true;
                loadOrderDetailsAsync();
            }
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        App app = Application.Current as App;
        if (app != null)
        {
            app.attendLists = null;
        }
    }

    private async Task loadOrderDetailsAsync()
    {
        int id = 1;
        App app = Application.Current as App;
        if (app.selectedProductDetailId != null)
            id = (int)app.selectedProductDetailId;

        //call product detail id 相對應的order detail 名單
        HttpClient client = new HttpClient();
        Uri uri = new Uri($"http://10.0.2.2:5016/api/OrderDetails/AttendList?id={id}");
        app.attendLists = await client.GetFromJsonAsync<List<CAttendList>>(uri);
        model.All = app.attendLists;

        //畫面上的資料
        int stock = (new CEventListViewModel()).getEvent(id).Stock;
        int odCount = app.attendLists.Count;
        lblStock.Text = $"{odCount}/{stock}";
        lblAttend.Text = $"{model.getAttendedCount()}/{model.getAttendListCount()}";

        float percent = (float)odCount/ stock;
        chartStock.Progress = Convert.ToInt32(percent * 100);

        percent = (float)model.getAttendedCount()/ model.getAttendListCount();
        chartAttend.Progress = Convert.ToInt32(percent * 100);

        loader.IsRunning = false;
        loader.IsVisible = false;
    }

    private void btnInfo_Clicked(object sender, EventArgs e)
    {
        this.OnAppearing();
    }

    private void btnAttendList_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PgAttendList());
    }

    private void btnScan_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PgScanTicket());
    }
}