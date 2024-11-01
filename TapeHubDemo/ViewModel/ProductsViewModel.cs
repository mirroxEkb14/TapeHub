using CommunityToolkit.Mvvm.ComponentModel;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using System.Collections.ObjectModel;

namespace TapeHubDemo.ViewModel;

public partial class ProductsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Product> _products = [];

    public ProductsViewModel(int shopBranchId) =>
        LoadProducts(shopBranchId);

    //
    // Summary:
    //     Asynchronously loads products associated with the specified shop branch ID.
    //     Retrieves the product list from the «ProductService» and populates the «Products»
    //         collection, which is bound to the UI.
    private async Task LoadProducts(int shopBranchId)
    {
        var productList = await ProductService.GetProductsByBranchIdAsync(shopBranchId);
        Products.Clear();

        foreach (var product in productList)
            Products.Add(product);
    }
}
