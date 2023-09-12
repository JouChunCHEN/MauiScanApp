using Camera.MAUI;
using MauiScanApp.Models;
using System.Net.Http.Json;

namespace MauiScanApp.Views;

public partial class PgScanTicket : ContentPage
{
	public PgScanTicket()
	{
		InitializeComponent();

        cameraView.BarCodeOptions = new()
        {
            AutoRotate = true,
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE }
        };
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras[0];
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            }
            );
        }
    }

    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            lblBarcode.Text = args.Result[0].Text;
        });
    }

    private async void btnScan_Clicked(object sender, EventArgs e)
    {
        int id = 0;
        App app = Application.Current as App;
        if (app.selectedProductDetailId != null)
            id = (int)app.selectedProductDetailId;

        ActivityIndicator loader = new ActivityIndicator();
        loader.IsRunning = true;
        loader.IsVisible = true;
        stackLayout.Children.Add(loader);
        await checkQrcode(id,txtQrCode.Text);
        loader.IsRunning = false;
        loader.IsVisible = false;
    }

    private async Task checkQrcode(int productDetailId, string qrcode)
    {
        using (HttpClient client = new HttpClient())
        {
            Uri uri = new Uri($"http://10.0.2.2:5016/api/Attend/ScanQrCode?productDetailId={productDetailId}&qrcode={qrcode}");
            HttpResponseMessage message = client.GetAsync(uri).Result;
            string result = message.Content.ReadAsStringAsync().Result;
            message.EnsureSuccessStatusCode();
            if (message.IsSuccessStatusCode)
            {
                DisplayAlert("³qª¾", result, "OK");

                using (HttpClient c = new HttpClient())
                {
                    App app = Application.Current as App;
                    Uri u = new Uri($"http://10.0.2.2:5016/api/OrderDetails/AttendList?id={productDetailId}");
                    app.attendLists = await c.GetFromJsonAsync<List<CAttendList>>(u);
                }
            }
        }
    }
}