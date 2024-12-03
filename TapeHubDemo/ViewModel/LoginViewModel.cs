using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.Database;
using TapeHubDemo.Enumeration;

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

    //
    // Summary:
    //     Represents a reference to the page for animations and displaying alerts.
    public Page? Page { get; set; }

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
            var isAdmin = user.Role == UserRole.Admin;
            ErrorMessage = string.Empty;
            await Shell.Current.GoToAsync($"//ShopBranchesPage?isAdmin={isAdmin}");
        }
        else
        {
            ErrorMessage = "Invalid username or password.";
        }
    }

    //
    // Summary:
    //     Highlights the tapped label.
    //     Displays an alert with the contact information for password recovery.
    [RelayCommand]
    public async Task ForgotPasswordAsync(Label label)
    {
        if (label != null)
            await HighlightLabel(label);

        if (Page != null)
            await Page.DisplayAlert("Forgotten Password", "Please, contact the Administrator: support@tapehub.com", "OK");
    }

    //
    // Summary:
    //     Highlights the tapped label.
    //     Navigates from «LoginPage» to «RegisterPage».
    [RelayCommand]
    public static async Task NavigateToRegisterAsync(Label label)
    {
        if (label != null)
            await HighlightLabel(label);

        await Shell.Current.GoToAsync($"RegisterPage");
    }

    #region Animation Methods
    //
    // Summary:
    //     Adds a highlight effect to a tapped label by changing its opacity.
    private static async Task HighlightLabel(Label label)
    {
        if (label == null)
            return;

        await label.FadeTo(0.5, 100, Easing.CubicOut);
        await label.FadeTo(1.0, 100, Easing.CubicIn);
    }
    #endregion
}
