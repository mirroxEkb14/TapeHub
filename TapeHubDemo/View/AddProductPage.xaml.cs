using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class AddProductPage : ContentPage, IQueryAttributable
{
	public AddProductPage() =>
		InitializeComponent();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("branchId"))
        {
            int branchId = Convert.ToInt32(query["branchId"]);
            BindingContext = new AddProductViewModel(branchId);
        }
    }
}