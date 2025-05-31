using D424___Software_Engineering_Capstone.Views;
using Microsoft.Extensions.Logging;
using D424___Software_Engineering_Capstone.Database;

namespace D424___Software_Engineering_Capstone;

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
				fonts.AddFont("Playball-Regular.ttf", "Playball");
				fonts.AddFont("Lora-Italic-VariableFont_wght.ttf", "Lora-Italic");
			});

		builder.Services.AddSingleton<MainPageView>();
		builder.Services.AddSingleton<DatabaseHandler>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
