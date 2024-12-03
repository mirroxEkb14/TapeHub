using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

//
// Summary:
//     «IQueryAttributable» allows to receive and handle a «branchId» parameter passed during navigation.
//     It also allows to avoid settring the «ContentPage.BindingContext» in the XAML file.
//     Alternatively, an empty «ProductsViewModel» constructor could be created along with the
//         «ContentPage.BindingContext» definition in the XAML file.
public partial class ProductsPage : ContentPage, IQueryAttributable
{
    private ProductsViewModel? _viewModel;

    public ProductsPage() =>
        InitializeComponent();

    //
    // Summary:
    //     Sets the «BindingContext» dynamically based on the «branchId» passed through the query parameters,
    //         which ensures that the «ProductsViewModel» receives the «branchId» directly in its constructor.
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("branchId"))
        {
            int branchId = Convert.ToInt32(query["branchId"]);
            BindingContext = new ProductsViewModel(branchId);
            _viewModel = BindingContext as ProductsViewModel;
        }

        if (query.ContainsKey("isAdmin"))
        {
            bool isAdmin = Convert.ToBoolean(query["isAdmin"]);
            if (_viewModel != null)
                _viewModel.IsAdmin = isAdmin;
        }
    }
}
