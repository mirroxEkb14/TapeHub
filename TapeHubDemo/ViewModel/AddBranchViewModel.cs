#region Imports
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using TapeHubDemo.Utils;
using Location = TapeHubDemo.Model.Location;
#endregion

namespace TapeHubDemo.ViewModel;

public partial class AddBranchViewModel : ObservableObject
{
    [ObservableProperty] private Location _location;
    [ObservableProperty] private ContactInfo _contactInfo;
    [ObservableProperty] private ShopBranch _shopBranch;
    [ObservableProperty] private string _errorMessage;

    public AddBranchViewModel()
    {
        Location = new();
        ContactInfo = new();
        ShopBranch = new();
        ErrorMessage = string.Empty;
    }

    [RelayCommand]
    public async Task AddBranchAsync()
    {
        if (!await ShopBranchValidator.ValidateUserInputs(Location, ContactInfo, ShopBranch))
            return;

        try
        {
            var locationId = await LocationService.AddLocationAsync(Location);
            if (locationId > 0)
            {
                ContactInfo.LocationID = locationId;
                var contactInfoId = await ContactInfoService.AddContactInfoAsync(ContactInfo);

                if (contactInfoId > 0)
                {
                    ShopBranch.ContactInfoID = contactInfoId;
                    var branchId = await ShopBranchService.AddShopBranchAsync(ShopBranch);
                    if (branchId > 0)
                    {
                        await OnSuccessfulAdding();
                        return;
                    }
                }
            }
            ErrorMessage = MessageContainer.ErrorMessageAddingShopBranch;
        }
        catch (Exception ex)
        {
            ErrorMessage = MessageContainer.GetUnexpectedErrorMessage(ex.Message);
        }
    }

    #region Private Helper Methods
    //
    // Summary:
    //     Displays a success message.
    //     Redirects to the «ShopBranchesPage».
    private async Task OnSuccessfulAdding()
    {
        ErrorMessage = String.Empty;
        await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationSuccess, MessageContainer.SuccessMessageAddingShopBranch, AlertDisplayer.OK);
        await Shell.Current.GoToAsync("..");
    }
    #endregion
}
