namespace TapeHubDemo.Utils;

//
// Summary:
//     This class contains a set of methods to display alerts in the application on the current page.
public static class AlertDisplayer
{
    public const string ValidationSuccess = "Success";
    public const string ValidationError = "Validation Error";
    public const string ValidationNoChanges = "No Changes";
    public const string ValidationForgottenPassword = "Forgotten Password";
    public const string ValidationOrderItem = "Order Item";
    public const string ValidationEditProduct =  "Edit Product";
    public const string ValidationDeleteProduct = "Delete Product";
    public const string ValidationEditBranch = "Edit Branch";
    public const string ValidationDeleteBranch = "Delete Branch";

    public const string OK = "OK";
    public const string Cancel = "Cancel";

    public static async Task DisplayAlertAsync(string title, string message, string cancel)
    {
        if (App.Current?.MainPage != null)
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}
