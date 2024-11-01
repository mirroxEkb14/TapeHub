using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.Database;
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

    public LoginViewModel() =>
        _errorMessage = string.Empty;

    //
    // Summary:
    //     Asynchronously attempts to log in the user by verifying the provided credentials.
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
