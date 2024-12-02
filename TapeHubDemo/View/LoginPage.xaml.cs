namespace TapeHubDemo.View;

public partial class LoginPage : ContentPage
{
	public LoginPage() =>
		InitializeComponent();

    //
    // Summary:
    //     Represents a tap handler in case of "Forgot your password?".
    private async void OnForgotPasswordTapped(object sender, EventArgs e) =>
        await DisplayAlert("Forgotten Password", "Please, contact the Administrator", "OK");
}
