using Camera.MAUI;

namespace MauiScanApp;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();

        cameraView.BarCodeOptions = new()
        {
            AutoRotate = true,
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE}
        };
	}

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if(cameraView.Cameras.Count > 0)
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
}

