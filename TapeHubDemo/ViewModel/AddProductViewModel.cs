#region Imports
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TapeHubDemo.Database;
using TapeHubDemo.Enumeration;
using TapeHubDemo.Model;
using TapeHubDemo.Utils;
#endregion

namespace TapeHubDemo.ViewModel;

public partial class AddProductViewModel : ObservableObject
{
    [ObservableProperty] private string? _title;
    [ObservableProperty] private string? _description;
    [ObservableProperty] private double _price;
    [ObservableProperty] private ObservableCollection<ProductType> _productTypes;
    [ObservableProperty] private ProductType _selectedType;
    [ObservableProperty] private string? _imagePath;
    [ObservableProperty] private string _errorMessage;

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
            ErrorMessage = MessageContainer.GetUnexpectedErrorMessage(ex.Message);
        }
    }

    #region Private Helper Methods
    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            ErrorMessage = MessageContainer.ProcutTitleRequired;
            return false;
        }
        if (string.IsNullOrWhiteSpace(Description))
        {
            ErrorMessage = MessageContainer.ProductDescriptionRequired;
            return false;
        }
        if (Price <= 0)
        {
            ErrorMessage = MessageContainer.ProductPricePositive;
            return false;
        }
        if (string.IsNullOrWhiteSpace(ImagePath))
        {
            ErrorMessage = MessageContainer.ProductImagePathRequired;
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
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationSuccess, MessageContainer.SuccessMessageAddingProduct, AlertDisplayer.OK);
            await Shell.Current.GoToAsync("..");
            return;
        }
        await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationError, MessageContainer.ErrorMessageAddingProduct, AlertDisplayer.OK);
    }
    #endregion
}
