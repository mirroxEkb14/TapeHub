using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.Database;
using TapeHubDemo.View;

namespace TapeHubDemo.ViewModel;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _confirmPassword;

    [ObservableProperty]
    private string _errorMessage;

    public RegisterViewModel() =>
        ErrorMessage = string.Empty;

    //
    // Summary:
    //     Represents a reference to the page for animations.
    public Page? Page { get; set; }

    [RelayCommand]
    public async Task RegisterAsync()
    {
        if (!ValidateInputs())
            return;

        if (await IsUsernameTaken(Username))
            return;

        try
        {
            var newUser = CreateNewUser();

            var result = await UserService.AddUserAsync(newUser);
            if (result > 0)
                await OnSuccessfulRegistration();
            else
                ErrorMessage = "Failed to create the account.";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
    }

    #region Validation Methods
    //
    // Summary:
    //     Validates the user inputs for registration.
    // Returns:
    //     «True» if inputs are valid, otherwise «False».
    private bool ValidateInputs()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
        {
            HandleError("All fields are required.");
            return false;
        }
        if (Password != ConfirmPassword)
        {
            HandleError("Passwords do not match.");
            return false;
        }
        return true;
    }

    //
    // Summary:
    //     Handles errors by setting the error message.
    private void HandleError(string message) =>
        ErrorMessage = message;


    //
    // Summary:
    //     Checks if the username is already taken.
    private async Task<bool> IsUsernameTaken(string username)
    {
        var existingUser = await UserService.GetUserByUsernameAsync(username);
        if (existingUser != null)
        {
            ErrorMessage = "Username already exists. Please choose a different one.";
            return true;
        }
        return false;
    }

    //
    // Summary:
    //     Creates a new user instance for registration.
    private Model.User CreateNewUser() =>
        new()
        {
            Username = Username,
            Password = Password,
            Role = Enumeration.UserRole.User
        };

    //
    // Summary:
    //     Handles the actions to take when registration is successful.
    private async Task OnSuccessfulRegistration()
    {
        ErrorMessage = string.Empty;

        if (Page != null)
            // -Page Animation-

        if (App.Current?.MainPage != null)
            await App.Current.MainPage.DisplayAlert("Success", "Your account has been created!", "OK");

        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
    #endregion
}
