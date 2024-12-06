namespace TapeHubDemo.Utils;

//
// Summary:
//     Holds a set of constants represeting the names of certain fields of certain models used 
//         as keys in query parameters. These query parameters are then used in the code-behind
//         of the certain views to pass data between views (to their ViewModels as constructor parameters).
public static class QueryParameterKeys
{
    public const string BranchId = "branchId";
    public const string ShopBranchId = "shopBranchId";
    public const string ProductId = "productId";
    public const string LoginUsername = "username";
    public const string LoginPassword = "password";
    public const string IsAdmin = "isAdmin";
}
