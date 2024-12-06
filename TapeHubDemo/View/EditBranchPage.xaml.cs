using TapeHubDemo.ViewModel;
using TapeHubDemo.Utils;

namespace TapeHubDemo.View;

public partial class EditBranchPage : ContentPage, IQueryAttributable
{
    private readonly EditBranchViewModel _viewModel;

    public EditBranchPage()
	{
		InitializeComponent();
        _viewModel = BindingContext as EditBranchViewModel ?? new EditBranchViewModel();
    }

    //
    // Summary:
    //     Receives the shop branch ID when navigating to the «EditBranchPage».
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey(QueryParameterKeys.ShopBranchId))
        {
            var parameterKey = query[QueryParameterKeys.ShopBranchId].ToString();
            if (parameterKey != null)
            {
                var shopBranchId = int.Parse(parameterKey);
                await _viewModel.LoadBranchDataAsync(shopBranchId);
            }
        }
    }
}