using TapeHubDemo.Utils;
using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class ShopBranchesPage : ContentPage, IQueryAttributable
{
    private readonly ShopBranchesViewModel? _viewModel;

    public ShopBranchesPage()
    {
        InitializeComponent();
        _viewModel = BindingContext as ShopBranchesViewModel;
    }

    //
    // Summary:
    //     Receives and sets «IsAdmin» dynamically.
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(QueryParameterKeys.IsAdmin, out var isAdminValue) &&
            bool.TryParse(isAdminValue.ToString(), out var isAdmin))
        {
            if (_viewModel != null)
                _viewModel.IsAdmin = isAdmin;
        }
    }

    //
    // Summary:
    //     Refreshes the shop branches when the page is navigated to (whenever the page is navigated back to).
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (_viewModel != null)
            await _viewModel.RefreshShopBranchesAsync();
    }
}
