using MauiScanApp.Models;
using MauiScanApp.Views;

namespace MauiScanApp;

public partial class App : Application
{
    public int? loggedSupplierId { get; set; }
    public int? selectedProductDetailId { get; set; }
	public List<CAttendList> attendLists { get; set; }
    public List<CEvent> eventLists { get; set; }
    public App()
	{
		InitializeComponent();

		MainPage = new PgLogin();
	}
}
