using TapeHubDemo.View;

namespace TapeHubDemo;

//
// Summary:
//     Is the code-behind file for AppShell.xaml.
//     Defines the routes and navigation logic between pages in the app.
public partial class AppShell : Shell
{
    private Label? _titleLabel;

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(ShopBranchesPage), typeof(ShopBranchesPage));
        Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
        Routing.RegisterRoute(nameof(AddBranchPage), typeof(AddBranchPage));
        Routing.RegisterRoute(nameof(EditBranchPage), typeof(EditBranchPage));

        SetTitleView();
    }

    #region TitleView
    //
    // Summary:
    //     Sets the title view in the navigation bar based on the current page.
    private void SetTitleView()
    {
        InitializeTitleLabel();
        SetTitleView(this, _titleLabel);
        Navigated += OnNavigated!;
    }

    
    //
    // Summary:
    //     Initializes the title label with the default properties.
    private void InitializeTitleLabel() {
        _titleLabel = new Label
        {
            FontFamily = "GreatVibesRegular",
            FontSize = 30,
            TextColor = Color.FromArgb("#F5F5DC"),
            VerticalOptions = LayoutOptions.Center
        };
    }

    //
    // Summary:
    //     Sets the title based on the current page.
    //     Checks if the current page is «ShopBranchesPage» to enable the flyout (burger) menu
    //         (for the previous ones («MainPage», «LoginPage») it’s automatically disabled).
    private void OnNavigated(object sender, ShellNavigatedEventArgs e)
    {
        _titleLabel!.Text = CurrentPage switch
        {
            LoginPage => "Login",
            ShopBranchesPage => "Shop Branches",
            ProductsPage => "Products",
            _ => "TapeHub",
        };
    }
    #endregion
}
