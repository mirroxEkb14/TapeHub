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

public partial class EditProductViewModel : ObservableObject
{
    [ObservableProperty] private string? _title;
    [ObservableProperty] private string? _description;
    [ObservableProperty] private double _price;
    [ObservableProperty] private ProductType _selectedType;
    [ObservableProperty] private string? _imagePath;
    [ObservableProperty] private ObservableCollection<ProductType> _productTypes;
    [ObservableProperty] private string _errorMessage;

    private readonly int _shopBranchId;
    private readonly Product _originalProduct;

    public EditProductViewModel(Product product, int shopBranchId)
    {
        Title = product.Title;
        Description = product.Description;
        Price = product.Price;
        SelectedType = product.Type;
        ImagePath = product.ImagePath;
        ProductTypes = new ObservableCollection<ProductType>(Enum.GetValues(typeof(ProductType)).Cast<ProductType>());
        _errorMessage = string.Empty;

        _originalProduct = product;
        _shopBranchId = shopBranchId;
    }

    [RelayCommand]
    public async Task SaveProductAsync()
    {
        if (!ValidateInputs())
            return;

        try
        {
            if (!IsProductChanged())
            {
                await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationNoChanges, MessageContainer.NoChangesEditing, AlertDisplayer.OK);
                return;
            }

            var updatedProduct = CreateUpdatedProduct();
            await OnSuccessfulSave(updatedProduct);
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
        ErrorMessage = string.Empty;
        return true;
    }

    private bool IsProductChanged() =>
        _originalProduct.Title != Title ||
        _originalProduct.Description != Description ||
        _originalProduct.Price != Price ||
        _originalProduct.Type != SelectedType ||
        _originalProduct.ImagePath != ImagePath;

    private Product CreateUpdatedProduct() =>
        new()
        {
            ID = _originalProduct.ID,
            Title = Title,
            Description = Description,
            Price = Price,
            Type = SelectedType,
            ImagePath = ImagePath,
            ShopBranchID = _shopBranchId
        };

    private async Task OnSuccessfulSave(Product updatedProduct)
    {
        var result = await ProductService.UpdateProductAsync(updatedProduct);
        if (result > 0)
        {
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationSuccess, MessageContainer.SuccessMessageEditingProduct, AlertDisplayer.OK);
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            ErrorMessage = MessageContainer.ErrorMessageEditingProduct;
        }
    }
    #endregion
}
