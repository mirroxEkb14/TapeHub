using System.Text.RegularExpressions;
using TapeHubDemo.Control;
using TapeHubDemo.Model;
using Location = TapeHubDemo.Model.Location;

namespace TapeHubDemo.Validator;

//
// Summary:
//     Contains a set of methods to validate admin inputs while adding/editing a «ShopBranch» entity.
public static class ShopBranchValidator
{
    //
    // Summary:
    //     Validates the «Location», «ContactInfo», and «ShopBranch» entities.
    public static async Task<bool> ValidateUserInputs(Location location, ContactInfo contactInfo, ShopBranch shopBranch)
    {
        var (IsValidLocation, LocationMessage) = ValidateLocation(location);
        if (!IsValidLocation)
        {
            await AlertDisplayer.DisplayAlertAsync("Validation Error", LocationMessage, "OK");
            return false;
        }
        var (IsValidContact, ContactMessage) = ValidateContactInfo(contactInfo);
        if (!IsValidContact)
        {
            await AlertDisplayer.DisplayAlertAsync("Validation Error", ContactMessage, "OK");
            return false;
        }
        var (IsValidShopBranch, ShopBranchMessage) = ValidateShopBranch(shopBranch);
        if (!IsValidShopBranch)
        {
            await AlertDisplayer.DisplayAlertAsync("Validation Error", ShopBranchMessage, "OK");
            return false;
        }
        return true;
    }

    #region Private Helper Methods
    private static (bool IsValid, string Message) ValidateLocation(Location location)
    {
        if (string.IsNullOrEmpty(location.Country))
            return (false, "Country is required.");
        if (string.IsNullOrEmpty(location.City))
            return (false, "City is required.");
        if (string.IsNullOrEmpty(location.Street))
            return (false, "Street is required.");
        if (location.Number <= 0)
            return (false, "Street number must be a positive value.");
        if (string.IsNullOrEmpty(location.ZIP))
            return (false, "ZIP code is required.");

        return (true, string.Empty);
    }

    private static (bool IsValid, string Message) ValidateContactInfo(ContactInfo contactInfo)
    {
        if (string.IsNullOrEmpty(contactInfo.Email))
            return (false, "Email is required.");
        if (!IsValidEmail(contactInfo.Email))
            return (false, "Invalid email format.");
        if (string.IsNullOrEmpty(contactInfo.Phone))
            return (false, "Phone number is required.");
        if (!IsValidPhoneNumber(contactInfo.Phone))
            return (false, "Invalid phone number format.");

        return (true, string.Empty);
    }

    private static (bool IsValid, string Message) ValidateShopBranch(ShopBranch shopBranch)
    {
        if (string.IsNullOrEmpty(shopBranch.Name))
            return (false, "Shop branch name is required.");
        if (string.IsNullOrEmpty(shopBranch.OpeningHours))
            return (false, "Opening hours are required.");
        if (string.IsNullOrEmpty(shopBranch.ClosingHours))
            return (false, "Closing hours are required.");
        if (shopBranch.StockQuantity < 0)
            return (false, "Stock quantity cannot be negative.");
        if (string.IsNullOrEmpty(shopBranch.ImagePath))
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
    #endregion
}
