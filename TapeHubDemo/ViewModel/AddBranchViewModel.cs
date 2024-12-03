using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.Control;
using TapeHubDemo.Database;
using TapeHubDemo.Model;

namespace TapeHubDemo.ViewModel;

public partial class AddBranchViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _openingHours;
    [ObservableProperty]
    private string _closingHours;
    [ObservableProperty]
    private string _stockQuantity;
    [ObservableProperty]
    private string _imagePath;

    [ObservableProperty]
    private string _errorMessage;

    public AddBranchViewModel()
    {
        Name = string.Empty;
        OpeningHours = string.Empty;
        ClosingHours = string.Empty;
        StockQuantity = string.Empty;
        ImagePath = string.Empty;

        ErrorMessage = string.Empty;
    }

    [RelayCommand]
    public async Task AddBranchAsync()
    {
        if (!ValidateInputs())
            return;

        try
        {
            var newBranch = CreateShopBranch();
            await ShopBranchService.AddShopBranchAsync(newBranch);
            await OnSuccessfulAdding();
        }
        catch (Exception ex)
        {
            await AlertDisplayer.DisplayAlertAsync("Error", $"Failed to add the branch: {ex.Message}", "OK");
        }
    }

    #region Private Helper Methods
    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(OpeningHours) ||
            string.IsNullOrWhiteSpace(ClosingHours) ||
            string.IsNullOrWhiteSpace(StockQuantity) ||
            string.IsNullOrWhiteSpace(ImagePath))
        {
            ErrorMessage = "All fields must be filled.";
            return false;
        }
        else if (!int.TryParse(StockQuantity, out _))
        {
            ErrorMessage = "Stock quantity must be a number.";
            return false;
        }
        return true;
    }

    private ShopBranch CreateShopBranch() =>
        new()
        {
            Name = Name,
            OpeningHours = OpeningHours,
            ClosingHours = ClosingHours,
            StockQuantity = int.Parse(StockQuantity),
            ImagePath = ImagePath
        };

    //
    // Summary:
    //     Displays a success message.
    //     Redirects to the «ShopBranchesPage».
    private async Task OnSuccessfulAdding()
    {
        ErrorMessage = String.Empty;
        await AlertDisplayer.DisplayAlertAsync("Success", "Shop branch added successfully!", "OK");
        await Shell.Current.GoToAsync("..");
    }
    #endregion
}
