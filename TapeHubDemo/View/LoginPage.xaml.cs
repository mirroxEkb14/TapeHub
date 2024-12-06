using TapeHubDemo.Utils;
using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class LoginPage : ContentPage, IQueryAttributable
{
    private readonly LoginViewModel? _viewModel;

    //
    // Summary:
    //     Passes the «LoginPage» instance to the «LoginViewModel» for implementing an animation and alert displaying.
    public LoginPage()
    {
        InitializeComponent();

        _viewModel = BindingContext as LoginViewModel;
        if (_viewModel != null)
            _viewModel.Page = this;
    }

    //
    // Summary:
    //     Handles the incoming «Username» and «Password» parameters from the «RegisterPage».
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey(QueryParameterKeys.LoginUsername))
            ((LoginViewModel)BindingContext).Username = query[QueryParameterKeys.LoginUsername] as string;

        if (query.ContainsKey(QueryParameterKeys.LoginPassword))
            ((LoginViewModel)BindingContext).Password = query[QueryParameterKeys.LoginPassword] as string;
    }
}
