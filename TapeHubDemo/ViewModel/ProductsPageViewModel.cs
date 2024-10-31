using System.Collections.ObjectModel;
using TapeHubDemo.Database;
using TapeHubDemo.Model;

namespace TapeHubDemo.ViewModel;

public class ProductsPageViewModel : BaseViewModel
{
    public ObservableCollection<Product> Products { get; set; }

    public ProductsPageViewModel(int shopBranchId)
    {
        Products = [];
        LoadProducts(shopBranchId);
    }

    private async void LoadProducts(int shopBranchId)
    {
        var products = await ProductService.GetProductsByBranchIdAsync(shopBranchId);
        foreach (var product in products)
            Products.Add(product);
    }
}
