using TapeHubDemo.View;

namespace TapeHubDemo.Utils;

//
// Summary:
//     Contains a list of arguments that can be passed to the navigation state to navigate to a specific page
//         (either passing additional parameters, or not).
public static class NavigationStateArguments
{
    private static readonly string ToShopBranchesPageWithIsAdmin = "{0}?isAdmin={1}";
    private static readonly string ToAddProductPageWithShopBranchId = "{0}?branchId={1}";
    private static readonly string ToEditProductPageWithProductIdShopBranchId = "{0}?productId={1}&branchId={2}";
    private static readonly string ToLoginPageWithUsernamePassword = "//{0}?username={1}&password={2}";
    private static readonly string ToProductsPageWithShopBranchIdIsAdmin = "{0}?branchId={1}&isAdmin={2}";
    private static readonly string ToEditBranchPageWithShopBranchId = "{0}?shopBranchId={1}";

    public const string ToLoginPage = $"//{nameof(LoginPage)}";
    public const string ToRegisterPage = $"{nameof(RegisterPage)}";
    public const string ToAddBranchPage = $"{nameof(AddBranchPage)}";

    public static string GetToShopBranchesPageWithIsAdmin(bool isAdmin) =>
        string.Format(ToShopBranchesPageWithIsAdmin, nameof(ShopBranchesPage), isAdmin);

    public static string GetToAddProductPageWithShopBranchId(int shopBranchId) =>
        string.Format(ToAddProductPageWithShopBranchId, nameof(AddProductPage), shopBranchId);

    public static string GetToEditProductPageWithProductIdShopBranchId(int productId, int shopBranchId) =>
        string.Format(ToEditProductPageWithProductIdShopBranchId, nameof(EditProductPage), productId, shopBranchId);

    public static string GetToLoginPageWithUsernamePassword(string username, string password) =>
        string.Format(ToLoginPageWithUsernamePassword, nameof(LoginPage), username, password);

    public static string GetToProductsPageWithShopBranchIdIsAdmin(int shopBranchId, bool isAdmin) =>
        string.Format(ToProductsPageWithShopBranchIdIsAdmin, nameof(ProductsPage), shopBranchId, isAdmin);

    public static string GetToEditBranchPageWithShopBranchId(int shopBranchId) =>
        string.Format(ToEditBranchPageWithShopBranchId, nameof(EditBranchPage), shopBranchId);
}
