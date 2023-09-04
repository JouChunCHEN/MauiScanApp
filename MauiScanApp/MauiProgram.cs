using Camera.MAUI;
using Microsoft.Extensions.Logging;
using Material.Components.Maui.Extensions;

namespace MauiScanApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCameraView()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .UseMaterialComponents(new List<string>
            {
                //generally, we needs add 6 types of font families
                "OpenSans-Regular.ttf",
                "OpenSans-Semibold.ttf",
             });
#if DEBUG
        builder.Logging.AddDebug();
#else
            client = new HttpClient();
#endif
        return builder.Build();
    }


}
