using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class RegisterPage : ContentPage
{
    //
    // Summary:
    //     Passes the «RegisterPage» instance to the «RegisterViewModel» for implementing an animation.
    public RegisterPage()
    {
        InitializeComponent();

        var viewModel = BindingContext as RegisterViewModel;
        if (viewModel != null)
            viewModel.Page = this;
    }
}