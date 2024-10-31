using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class ProductsPage : ContentPage
{
	public ProductsPage(int shopBranchId)
	{
		InitializeComponent();
        BindingContext = new ProductsPageViewModel(shopBranchId);
    }
}