namespace TapeHubDemo;

//
// Summary:
//     Is the code-behind file for MainPage.xaml.
//     Defines behavior, event handlers, and interaction logic
//         (handling button clicks or updating UI elements based on user interactions).
public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
