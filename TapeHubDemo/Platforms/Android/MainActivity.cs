using Android.App;
using Android.Content.PM;

namespace TapeHubDemo;

//
// Summary:
//     This class represents an entry point of the Android app.
//     Initializes the MAUI app on Android and is responsible for launching the application.
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}
