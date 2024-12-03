using TapeHubDemo.ViewModel;

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
        if (query.ContainsKey("shopBranchId"))
        {
            var shopBranchId = int.Parse(query["shopBranchId"].ToString());
            await _viewModel.LoadBranchDataAsync(shopBranchId);
        }
    }
}