using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TapeHubDemo.Control;
using TapeHubDemo.Database;
using TapeHubDemo.Enumeration;
using TapeHubDemo.Model;

namespace TapeHubDemo.ViewModel;

public partial class AddProductViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private string? _description;
    [ObservableProperty]
    private double _price;
    [ObservableProperty]
    private ObservableCollection<ProductType> _productTypes;
    [ObservableProperty]
    private ProductType _selectedType;
    [ObservableProperty]
    private string? _imagePath;
    [ObservableProperty]
    private string _errorMessage;

    private readonly int _shopBranchId;

    public AddProductViewModel(int shopBranchId)
    {
        _shopBranchId = shopBranchId;
        _productTypes = new ObservableCollection<ProductType>(Enum.GetValues(typeof(ProductType)).Cast<ProductType>());
        _selectedType = _productTypes.First();
        _errorMessage = string.Empty;
    }

    [RelayCommand]
    public async Task SaveProductAsync()
    {
        if (!ValidateInputs())
            return;

        try
        {
            await OnSuccessfullAdding(CreateProduct());
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
    }

    #region Private Helper Methods
    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            ErrorMessage = "Title is required.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(Description))
        {
            ErrorMessage = "Description is required.";
            return false;
        }
        if (Price <= 0)
        {
            ErrorMessage = "Price must be greater than 0.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(ImagePath))
        {
            ErrorMessage = "Image path is required.";
            return false;
        }
        return true;
    }

    private Product CreateProduct() =>
        new()
        {
            Title = Title,
            Description = Description,
            Price = Price,
            Type = SelectedType,
            ImagePath = ImagePath,
            ShopBranchID = _shopBranchId
        };

    private async Task OnSuccessfullAdding(Product newProduct)
    {
        var productId = await ProductService.AddProductAsync(newProduct);
        if (productId > 0)
        {
            ErrorMessage = string.Empty;
            await AlertDisplayer.DisplayAlertAsync("Success", "Product added successfully!", "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }
        await AlertDisplayer.DisplayAlertAsync("Error", "Failed to add product, please try again.", "OK");
    }
    #endregion
}
