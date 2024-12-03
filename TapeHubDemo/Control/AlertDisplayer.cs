namespace TapeHubDemo.Control;

//
// Summary:
//     This class contains a set of methods to display alerts in the application on the current page.
public static class AlertDisplayer
{
    public static async Task DisplayAlertAsync(string title, string message, string cancel)
    {
        if (App.Current?.MainPage != null)
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}
