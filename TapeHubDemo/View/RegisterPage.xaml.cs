using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterPageViewModel();
    }
}