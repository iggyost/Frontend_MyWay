using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace Frontend_MyWay;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        EntryHandler.PlatformViewFactory = (h) => {
            var editText = new AndroidX.AppCompat.Widget.AppCompatEditText(h.Context);
            editText.Background = null;
            editText.SetBackgroundColor(Android.Graphics.Color.Transparent);
            return editText;
        };

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
    }
}
