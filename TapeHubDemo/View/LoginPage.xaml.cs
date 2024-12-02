using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class LoginPage : ContentPage
{
    //
    // Summary:
    //     Passes the «LoginPage» instance to the «LoginViewModel» for implementing an animation and alert displaying.
    public LoginPage()
    {
        InitializeComponent();
        
        var viewModel = BindingContext as LoginViewModel;
        if (viewModel != null)
            viewModel.Page = this;
    }
}
