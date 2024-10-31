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
    //     Deletes the DB.
    //     Initializes the DB.
    private static async void OnLaunch()
    {
        //DatabaseService.DeleteDatabase();
        await DatabaseService.InitializeDatabase();
    }
}
