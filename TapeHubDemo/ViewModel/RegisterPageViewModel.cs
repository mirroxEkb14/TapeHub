using System.Windows.Input;
using TapeHubDemo.Enumeration;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using TapeHubDemo.View;

namespace TapeHubDemo.ViewModel;

public class RegisterPageViewModel : BaseViewModel
{
    private string _username;
    private string _password;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public ICommand SignUpCommand { get; }
    public ICommand LogInCommand { get; }
    public ICommand ForgotPasswordCommand { get; }

    public RegisterPageViewModel()
    {
        SignUpCommand = new Command(async () => await SignUp());
        LogInCommand = new Command(async () => await LogIn());
        ForgotPasswordCommand = new Command(async () => await ForgotPassword());
    }

    private async Task SignUp()
    {
        Console.WriteLine($"\n\nUsername: {Username}, Password: {Password}\n\\n");
        if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
        {
            try
            {
                var newUser = new User
                {
                    Username = Username,
                    Password = Password,  // In production, hash this
                    Role = UserRole.User
                };
                await UserService.AddUserAsync(newUser);
                await NavigateToUserListPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\nSignUp Error: {ex.Message}\n\n");
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred during sign up", "OK");
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Please enter username and password", "OK");
        }
    }

    private async Task LogIn()
    {
        var user = await UserService.GetUserByUsernameAndPasswordAsync(Username, Password);
        if (user != null)
            await NavigateToUserListPage();
        else
            await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
    }

    private async Task ForgotPassword() =>
        await Application.Current.MainPage.DisplayAlert("Forgot Password", "Please contact the administrator", "OK");

    private async Task NavigateToUserListPage() =>
        await Application.Current.MainPage.Navigation.PushAsync(new ShopBranchesPage());
}
