using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.Database;
using TapeHubDemo.Enumeration;
using TapeHubDemo.View;

namespace TapeHubDemo.ViewModel;

//
// Summary:
//     Handles user login logic and error display.
//     Extends «ObservableObject» to support property change notifications (CommunityToolkit.Mvvm).
public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _username;
    [ObservableProperty]
    private string? _password;
    [ObservableProperty]
    private string _errorMessage;

    public LoginViewModel()
    {
        _errorMessage = string.Empty;
        LoadAdminCredentialsAsync();
    }

    //
    // Summary:
    //     Loads the admin credentials and pre-fills the «Username» and «Password» fields.
    private async void LoadAdminCredentialsAsync()
    {
        try
        {
            var users = await UserService.GetAllUsersAsync();
            var adminUser = users.FirstOrDefault(user => user.Role == UserRole.Admin);

            if (adminUser != null)
            {
                Username = adminUser.Username;
                Password = adminUser.Password;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Unable to load admin credentials: {ex.Message}";
        }
    }

    //
    // Summary:
    //     Attempts to log in the user by verifying the provided credentials.
    //     If the login is successful, navigates to the «ShopBranchesPage».
    //     Otherwise, displays an error message indicating invalid credentials.
    [RelayCommand]
    public async Task LoginAsync()
    {
        var user = await UserService.GetUserByUsernameAndPasswordAsync(Username, Password);
        if (user != null)
        {
            ErrorMessage = string.Empty;
            await Shell.Current.GoToAsync($"//{nameof(ShopBranchesPage)}");
        }
        else
        {
            ErrorMessage = "Invalid username or password.";
        }
    }
}
