using TapeHubDemo.Database;

namespace TapeHubDemo;

//
// Summary:
//     Is the code-behind file for App.xaml.
//     Contains the main setup logic that runs when the app starts.
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        OnLaunch();
        MainPage = new AppShell();
    }

    //
    // Summary:
    //     Deletes and initializes the DB.
    private static async void OnLaunch()
    {
        DatabaseService.DeleteDatabase();
        await DatabaseService.InitializeDatabase();
    }
}
