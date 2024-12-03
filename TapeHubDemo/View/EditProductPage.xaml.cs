using TapeHubDemo.Control;
using TapeHubDemo.Database;
using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class EditProductPage : ContentPage, IQueryAttributable
{
	public EditProductPage() =>
		InitializeComponent();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("productId") && query.ContainsKey("branchId"))
        {
            int productId = Convert.ToInt32(query["productId"]);
            int branchId = Convert.ToInt32(query["branchId"]);
            var product = await ProductService.GetProductByIdAsync(productId);

            if (product != null)
                BindingContext = new EditProductViewModel(product, branchId);
        }
    }
}