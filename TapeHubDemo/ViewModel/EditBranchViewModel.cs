#region Imports
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using TapeHubDemo.Utils;
using Location = TapeHubDemo.Model.Location;
#endregion

namespace TapeHubDemo.ViewModel;

public partial class EditBranchViewModel : ObservableObject
{
    [ObservableProperty] private ShopBranch _shopBranch;
    [ObservableProperty] private Location _location;
    [ObservableProperty] private ContactInfo _contactInfo;

    private ShopBranch _originalShopBranch;
    private Location _originalLocation;
    private ContactInfo _originalContactInfo;

    public EditBranchViewModel()
    {
        _shopBranch = new();
        _location = new();
        _contactInfo = new();

        _originalShopBranch = new();
        _originalLocation = new();
        _originalContactInfo = new();
    }

    //
    // Summary:
    //     Loads the current data for editing.
    //     Loads data into the view model.
    //     Stores original copies for comparison.
    public async Task LoadBranchDataAsync(int shopBranchId)
    {
        var shopBranch = await ShopBranchService.GetShopBranchByIdAsync(shopBranchId);
        var contactInfo = await ContactInfoService.GetContactInfoByIdAsync(shopBranch.ContactInfoID);
        var location = await LocationService.GetLocationByIdAsync(contactInfo.LocationID ?? 0);

        ShopBranch = shopBranch;
        ContactInfo = contactInfo;
        Location = location;

        _originalShopBranch = shopBranch;
        _originalContactInfo = contactInfo;
        _originalLocation = location;
    }

    //
    // Summary:
    //     Validates the inputs.
    //     Checks if any changes were made.
    //     Saves the changes.
    [RelayCommand]
    public async Task SaveChangesAsync()
    {
        if (!await ShopBranchValidator.ValidateUserInputs(Location, ContactInfo, ShopBranch))
            return;

        if (await AreSameShopBranches())
            return;

        try
        {
            await LocationService.UpdateLocationAsync(Location);
            ContactInfo.LocationID = Location.ID;
            await ContactInfoService.UpdateContactInfoAsync(ContactInfo);
            ShopBranch.ContactInfoID = ContactInfo.ID;
            await ShopBranchService.UpdateShopBranchAsync(ShopBranch);

            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationSuccess, MessageContainer.SuccessMessageEditingShopBranch, AlertDisplayer.OK);
            await Shell.Current.GoToAsync($"..");
        }
        catch (Exception ex)
        {
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationError, MessageContainer.GetUnexpectedErrorMessage(ex.Message), AlertDisplayer.OK);
        }
    }

    #region Private Helper Methods
    //
    // Summary:
    //     Checks if the admin tries to save the same data for the «ShopBranch».
    private async Task<bool> AreSameShopBranches()
    {
        bool isLocationChanged =
            !string.Equals(_originalLocation.Country, Location.Country, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(_originalLocation.City, Location.City, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(_originalLocation.Street, Location.Street, StringComparison.OrdinalIgnoreCase) ||
            _originalLocation.Number != Location.Number ||
            !string.Equals(_originalLocation.ZIP, Location.ZIP, StringComparison.OrdinalIgnoreCase);

        bool isContactInfoChanged =
            !string.Equals(_originalContactInfo.Email, ContactInfo.Email, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(_originalContactInfo.Phone, ContactInfo.Phone, StringComparison.OrdinalIgnoreCase);

        bool isShopBranchChanged =
            !string.Equals(_originalShopBranch.Name, ShopBranch.Name, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(_originalShopBranch.OpeningHours, ShopBranch.OpeningHours, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(_originalShopBranch.ClosingHours, ShopBranch.ClosingHours, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(_originalShopBranch.ImagePath, ShopBranch.ImagePath, StringComparison.OrdinalIgnoreCase) ||
            _originalShopBranch.StockQuantity != ShopBranch.StockQuantity;

        bool areSameShopBranches = isLocationChanged || isContactInfoChanged || isShopBranchChanged;
        if (areSameShopBranches)
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationNoChanges, MessageContainer.NoChangesEditing, AlertDisplayer.OK);
        return areSameShopBranches;
    }
    #endregion
}
