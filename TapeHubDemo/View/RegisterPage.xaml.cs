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

        if (BindingContext is RegisterViewModel viewModel)
            viewModel.Page = this;
    }
}
