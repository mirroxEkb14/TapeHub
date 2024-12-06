#region Imports
using System.Text.RegularExpressions;
using TapeHubDemo.Control;
using TapeHubDemo.Model;
using Location = TapeHubDemo.Model.Location;
#endregion

namespace TapeHubDemo.Utils;

//
// Summary:
//     Contains a set of methods to validate admin inputs while adding/editing a «ShopBranch» entity.
public static class ShopBranchValidator
{
    private static readonly string phonePattern = @"^\+\d{1,3} \d{3} \d{3} \d{3}$";

    //
    // Summary:
    //     Validates the «Location», «ContactInfo», and «ShopBranch» entities.
    public static async Task<bool> ValidateUserInputs(Location location, ContactInfo contactInfo, ShopBranch shopBranch)
    {
        var (IsValidLocation, LocationMessage) = ValidateLocation(location);
        if (!IsValidLocation)
        {
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationError, LocationMessage, AlertDisplayer.OK);
            return false;
        }
        var (IsValidContact, ContactMessage) = ValidateContactInfo(contactInfo);
        if (!IsValidContact)
        {
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationError, ContactMessage, AlertDisplayer.OK);
            return false;
        }
        var (IsValidShopBranch, ShopBranchMessage) = ValidateShopBranch(shopBranch);
        if (!IsValidShopBranch)
        {
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationError, ShopBranchMessage, AlertDisplayer.OK);
            return false;
        }
        return true;
    }

    #region Private Helper Methods
    private static (bool IsValid, string Message) ValidateLocation(Location location)
    {
        if (string.IsNullOrEmpty(location.Country))
            return (false, MessageContainer.LocationCountryRequired);
        if (string.IsNullOrEmpty(location.City))
            return (false, MessageContainer.LocationCityRequired);
        if (string.IsNullOrEmpty(location.Street))
            return (false, MessageContainer.LocationStreetRequired);
        if (location.Number <= 0)
            return (false, MessageContainer.LocationStreetNumberPositive);
        if (string.IsNullOrEmpty(location.ZIP))
            return (false, MessageContainer.LocationZIPRequired);

        return (true, string.Empty);
    }

    private static (bool IsValid, string Message) ValidateContactInfo(ContactInfo contactInfo)
    {
        if (string.IsNullOrEmpty(contactInfo.Email))
            return (false, MessageContainer.ContactInfoEmailRequired);
        if (!IsValidEmail(contactInfo.Email))
            return (false, MessageContainer.ContactInfoEmailInvalid);
        if (string.IsNullOrEmpty(contactInfo.Phone))
            return (false, MessageContainer.ContactInfoPhoneRequired);
        if (!IsValidPhoneNumber(contactInfo.Phone))
            return (false, MessageContainer.ContactInfoEmailInvalid);

        return (true, string.Empty);
    }

    private static (bool IsValid, string Message) ValidateShopBranch(ShopBranch shopBranch)
    {
        if (string.IsNullOrEmpty(shopBranch.Name))
            return (false, MessageContainer.ShopBranchNameRequired);
        if (string.IsNullOrEmpty(shopBranch.OpeningHours))
            return (false, MessageContainer.ShopBranchOpeningHoursRequired);
        if (string.IsNullOrEmpty(shopBranch.ClosingHours))
            return (false, MessageContainer.ShopBranchClosingHoursRequired);
        if (shopBranch.StockQuantity < 0)
            return (false, MessageContainer.ShopBranchStockQuantityNegative);
        if (string.IsNullOrEmpty(shopBranch.ImagePath))
            return (false, MessageContainer.ShopBranchImagePathRequired);

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

        return Regex.IsMatch(phone, phonePattern);
    }
    #endregion
}
