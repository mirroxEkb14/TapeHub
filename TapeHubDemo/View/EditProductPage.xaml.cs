#region Imports
using TapeHubDemo.Database;
using TapeHubDemo.Utils;
using TapeHubDemo.ViewModel;
#endregion

namespace TapeHubDemo.View;

public partial class EditProductPage : ContentPage, IQueryAttributable
{
	public EditProductPage() =>
		InitializeComponent();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey(QueryParameterKeys.ProductId) && query.ContainsKey(QueryParameterKeys.BranchId))
        {
            int productId = Convert.ToInt32(query[QueryParameterKeys.ProductId]);
            int branchId = Convert.ToInt32(query[QueryParameterKeys.BranchId]);
            var product = await ProductService.GetProductByIdAsync(productId);

            if (product != null)
                BindingContext = new EditProductViewModel(product, branchId);
        }
    }
}