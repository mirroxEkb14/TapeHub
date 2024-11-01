namespace TapeHubDemo;

//
// Summary:
//     Represents the point, where the app gets initialized with its services, pages, and platform-specific settings.
//     Configures the services and sets up dependency injection (DI) for the MAUI app.
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
                fonts.AddFont("Cinzel-Regular.ttf", "CinzelRegular");
                fonts.AddFont("Cinzel-Bold.ttf", "CinzelBold");
                fonts.AddFont("GreatVibes-Regular.ttf", "GreatVibesRegular");
                fonts.AddFont("Merriweather-Regular.ttf", "MerriweatherRegular");
            });

        return builder.Build();
    }
}
