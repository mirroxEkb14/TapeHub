using CommunityToolkit.Mvvm.ComponentModel;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.Control;

namespace TapeHubDemo.ViewModel;

public partial class ProductsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Product> _products;
    [ObservableProperty]
    private Product? _selectedProduct;
    [ObservableProperty]
    private bool _isAdmin;

    private readonly int _shopBranchId;

    public ProductsViewModel(int shopBranchId)
    {
        _products = [];
        _isAdmin = false;
        _shopBranchId = shopBranchId;
        LoadProducts();
    }

    //
    // Summary:
    //     Loads products associated with the specified shop branch ID.
    //     Retrieves the product list from the «ProductService» and populates the «Products»
    //         collection, which is bound to the UI.
    private async Task LoadProducts()
    {
        var productList = await ProductService.GetProductsByBranchIdAsync(_shopBranchId);
        Products.Clear();

        foreach (var product in productList)
            Products.Add(product);
    }

    [RelayCommand]
    public async Task ProductSelected()
    {
        if (SelectedProduct != null)
        {
            ClearSelections();
            return;
        }
        await AlertDisplayer.DisplayAlertAsync("Order Item", "Please visit our shop to order this item in person.", "OK");
    }

    [RelayCommand]
    public void ProductDoublePressed(Product selectedProduct)
    {
        ClearSelections();

        selectedProduct.IsSelected = true;
        SelectedProduct = selectedProduct;
    }

    [RelayCommand]
    public async Task AddProductAsync() =>
        await Shell.Current.GoToAsync($"AddProductPage?branchId={_shopBranchId}");

    [RelayCommand]
    public async Task EditProductAsync()
    {
        if (SelectedProduct == null)
        {
            await AlertDisplayer.DisplayAlertAsync("Edit Product", "No product selected for editing.", "OK");
            return;
        }
        await Shell.Current.GoToAsync($"EditProductPage?productId={SelectedProduct.ID}&branchId={_shopBranchId}");
    }

    [RelayCommand]
    public async Task DeleteProductAsync()
    {
        if (SelectedProduct == null)
        {
            await AlertDisplayer.DisplayAlertAsync("Remove Product", "No product selected for removing.", "OK");
            return;
        }

        var result = await ProductService.DeleteProductAsync(SelectedProduct.ID);
        if (result > 0)
        {
            Products.Remove(SelectedProduct);
            ClearSelections();
            await AlertDisplayer.DisplayAlertAsync("Remove Product", "Selected product was successfully removed.", "OK");
        }
    }

    #region Private Helper Methods
    //
    // Summary:
    //     Clears previous selections when double clicked.
    private void ClearSelections()
    {
        foreach (var product in Products)
            product.IsSelected = false;
        SelectedProduct = null;
    }
    #endregion
}
