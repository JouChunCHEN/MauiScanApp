using MauiScanApp.Models;
using MauiScanApp.ViewModels;
using System.Net.Http.Json;

namespace MauiScanApp.Views;

public partial class PgEventList_New : ContentPage
{
    CEventListViewModel model = new CEventListViewModel();
	public PgEventList_New()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        App app = Application.Current as App;
        if (app.eventLists == null)
        {
            loader.IsRunning = true;
            await loadProductDetailsAsync();
        }
    }
    private async Task loadProductDetailsAsync()
    {
        int id = 1;
        App app = Application.Current as App;
        if (app.loggedSupplierId!=null)
            id = (int)app.loggedSupplierId;

        //call符合登入廠商id的api
        HttpClient client = new HttpClient();
        Uri uri = new Uri($"http://10.0.2.2:5016/api/ProductDetails/EventList?id={id}");
        app.eventLists = await client.GetFromJsonAsync<List<CEvent>>(uri);
        model.All = app.eventLists;
        lvNewEvent.ItemsSource = model.getNewEevents();

        if(model.getNewEevents() == null)
        {
            lblTitle.Text = "尚無活動";
            lblTitle.IsVisible = true;
        }
            

        loader.IsRunning = false;
        loader.IsVisible = false;
    }

        private void lvNewEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        App app = Application.Current as App;
        CEvent p = e.CurrentSelection.FirstOrDefault() as CEvent;
        if (app != null && p !=null)
            app.selectedProductDetailId = p.ProductDetail_ID;

        Navigation.PushAsync(new PgAttendInfo());
    }

}