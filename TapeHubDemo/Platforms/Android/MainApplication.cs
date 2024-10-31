using Android.App;
using Android.Runtime;

namespace TapeHubDemo;

//
// Summary:
//     This class defines the Android application class, which is called before any activity, such as MainActivity,
//         which is part of the Android lifecycle and helps in global configuration or dependency injection initialization.
// Attributes:
//     [Application] - Marks this class as the main application entry point for Android.
// Inheritance:
//     MauiApplication - Is responsible for initializing the entire MAUI framework for the Android platform.
// Primary Constructor Parameters:
//     IntPtr handle - A pointer to the unmanaged (native) memory for the Android application object.
//     JniHandleOwnership ownership - Is an enum that tells the system whether the handle is transferred (from Android) or
//         whether it's owned by the application.
[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership ownership) : MauiApplication(handle, ownership)
{
    //
    // Summary:
    //     Overrides the method of the base class.
    // Returns:
    //     An instance of MauiApp (the root object of the MAUI application).
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
