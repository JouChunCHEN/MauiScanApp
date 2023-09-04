using MauiScanApp.Models;
using MauiScanApp.ViewModels;

namespace MauiScanApp.Views;

public partial class PgEventList_Old : ContentPage
{
    CEventListViewModel model = new CEventListViewModel();
    public PgEventList_Old()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lvOldEvent.ItemsSource = model.getOldEevents();
        if (model.getOldEevents() == null)
        {
            lblTitle.Text = "©|µL¬¡°Ê";
            lblTitle.IsVisible = true;
        }
    }

    private void lvOldEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        App app = Application.Current as App;
        CEvent p = e.CurrentSelection.FirstOrDefault() as CEvent;
        if (app != null && p != null)
            app.selectedProductDetailId = p.ProductDetail_ID;

        Navigation.PushAsync(new PgAttendInfo());
    }
}