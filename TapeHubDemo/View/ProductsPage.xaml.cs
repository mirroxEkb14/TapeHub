using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

//
// Summary:
//     «IQueryAttributable» allows to receive and handle a «branchId» parameter passed during navigation.
public partial class ProductsPage : ContentPage, IQueryAttributable
{
	public ProductsPage() =>
		InitializeComponent();

    //
    // Summary:
    //     Sets the «BindingContext» dynamically based on the «branchId» passed through the query parameters,
    //         which ensures that the «ProductsViewModel» receives the «branchId» directly in its constructor.
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("branchId"))
        {
            int branchId = Convert.ToInt32(query["branchId"]);
            BindingContext = new ProductsViewModel(branchId);
        }
    }
}