using TapeHubDemo.ViewModel;
using TapeHubDemo.Utils;

namespace TapeHubDemo.View;

public partial class AddProductPage : ContentPage, IQueryAttributable
{
	public AddProductPage() =>
		InitializeComponent();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey(QueryParameterKeys.BranchId))
        {
            int branchId = Convert.ToInt32(query[QueryParameterKeys.BranchId]);
            BindingContext = new AddProductViewModel(branchId);
        }
    }
}
