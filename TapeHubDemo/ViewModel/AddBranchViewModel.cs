using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using TapeHubDemo.Control;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using Location = TapeHubDemo.Model.Location;

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
        if (!await ValidateUserInputs())
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
            ErrorMessage = "Failed to add shop branch. Please try again.";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
    }

    #region Private Helper Methods
    private async Task<bool> ValidateUserInputs()
    {
        var (IsValidLocation, LocationMessage) = ValidateLocation();
        if (!IsValidLocation)
        {
            await AlertDisplayer.DisplayAlertAsync("Validation Error", LocationMessage, "OK");
            return false;
        }
        var (IsValidContact, ContactMessage) = ValidateContactInfo();
        if (!IsValidContact)
        {
            await AlertDisplayer.DisplayAlertAsync("Validation Error", ContactMessage, "OK");
            return false;
        }
        var (IsValidShopBranch, ShopBranchMessage) = ValidateShopBranch();
        if (!IsValidShopBranch)
        {
            await AlertDisplayer.DisplayAlertAsync("Validation Error", ShopBranchMessage, "OK");
            return false;
        }
        return true;
    }

    private (bool IsValid, string Message) ValidateLocation()
    {
        if (string.IsNullOrEmpty(Location.Country))
            return (false, "Country is required.");
        if (string.IsNullOrEmpty(Location.City))
            return (false, "City is required.");
        if (string.IsNullOrEmpty(Location.Street))
            return (false, "Street is required.");
        if (Location.Number <= 0)
            return (false, "Street number must be a positive value.");
        if (string.IsNullOrEmpty(Location.ZIP))
            return (false, "ZIP code is required.");

        return (true, string.Empty);
    }

    private (bool IsValid, string Message) ValidateContactInfo()
    {
        if (string.IsNullOrEmpty(ContactInfo.Email))
            return (false, "Email is required.");
        if (!IsValidEmail(ContactInfo.Email))
            return (false, "Invalid email format.");
        if (string.IsNullOrEmpty(ContactInfo.Phone))
            return (false, "Phone number is required.");
        if (!IsValidPhoneNumber(ContactInfo.Phone))
            return (false, "Invalid phone number format.");

        return (true, string.Empty);
    }

    private (bool IsValid, string Message) ValidateShopBranch()
    {
        if (string.IsNullOrEmpty(ShopBranch.Name))
            return (false, "Shop branch name is required.");
        if (string.IsNullOrEmpty(ShopBranch.OpeningHours))
            return (false, "Opening hours are required.");
        if (string.IsNullOrEmpty(ShopBranch.ClosingHours))
            return (false, "Closing hours are required.");
        if (ShopBranch.StockQuantity < 0)
            return (false, "Stock quantity cannot be negative.");
        if (string.IsNullOrEmpty(ShopBranch.ImagePath))
            return (false, "Image path is required.");

        return (true, string.Empty);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var emailCheck = new System.Net.Mail.MailAddress(email);
            return emailCheck.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidPhoneNumber(string phone)
    {
        if (string.IsNullOrEmpty(phone))
            return false;

        var phonePattern = @"^\+\d{1,3} \d{3} \d{3} \d{3}$";
        return Regex.IsMatch(phone, phonePattern);
    }

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
