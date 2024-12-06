namespace TapeHubDemo.Utils;

public static class MessageContainer
{
    private static readonly string UnexpectedError = "An unexpected error occurred: {0}";
    private static readonly string ErrorLoadingAdminCredentials = "Unable to load admin credentials: {0}";

    public const string SloganText = "Discover the Classics";

    public const string LoginUsernameRequired = "Username is required.";
    public const string LoginPasswordRequired = "Password is required.";
    public const string LoginInvalidCredentials = "Invalid username or password.";
    public const string LoginAdminContact = "Please, contact the Administrator: support@tapehub.com";

    public const string RegisterUsernameRequired = "Username is required.";
    public const string RegisterPasswordRequired = "Password is required.";
    public const string RegisterConfirmPasswordRequired = "Confirm password is required.";
    public const string RegisterPasswordMismatch = "Passwords do not match.";
    public const string RegisterFailedAccountCreation = "Failed to create the account.";
    public const string RegisterUsernameTaken = "Username already exists. Please choose a different one.";
    public const string RegisterAccountCreated = "Account created successfully!";

    public const string LocationCountryRequired = "Country is required.";
    public const string LocationCityRequired = "City is required.";
    public const string LocationStreetRequired = "Street is required.";
    public const string LocationStreetNumberPositive = "Street number must be a positive value.";
    public const string LocationZIPRequired = "ZIP code is required.";

    public const string ContactInfoEmailRequired = "Email is required.";
    public const string ContactInfoEmailInvalid = "Invalid email format.";
    public const string ContactInfoPhoneRequired = "Phone number is required.";
    public const string ShopBranchPhoneInvalid = "Invalid phone number format.";

    public const string ShopBranchNameRequired = "Shop branch name is required.";
    public const string ShopBranchOpeningHoursRequired = "Opening hours are required.";
    public const string ShopBranchClosingHoursRequired = "Closing hours are required.";
    public const string ShopBranchStockQuantityNegative = "Stock quantity cannot be negative.";
    public const string ShopBranchImagePathRequired = "Image path is required.";

    public const string ProcutTitleRequired = "Title is required.";
    public const string ProductDescriptionRequired = "Description is required.";
    public const string ProductPricePositive = "Price must be greater than 0.";
    public const string ProductImagePathRequired = "Image path is required.";

    public const string VisitingPhysicalShop = "Please visit our shop to order this item in person.";

    public const string NoChangesEditing = "No changes were made.";

    public const string NoShopBranchSelected = "No shop branch selected.";
    public const string NoProductSelected = "No product selected.";

    public const string SuccessMessageAddingShopBranch = "Shop branch added successfully!";
    public const string SuccessMessageEditingShopBranch = "Shop branch updated successfully!";
    public const string SuccessMessageDeletingShopBranch = "Shop branch deleted successfully!";
    public const string SuccessMessageAddingProduct = "Product added successfully!";
    public const string SuccessMessageEditingProduct = "Product updated successfully!";
    public const string SuccessMessageDeletingProduct = "Product deleted successfully!";

    public const string ErrorMessageAddingShopBranch = "Failed to add shop branch. Please try again.";
    public const string ErrorMessageDeletingShopBranch = "Failed to delete shop branch. Please try again.";
    public const string ErrorMessageAddingProduct = "Failed to add product. Please try again.";
    public const string ErrorMessageEditingProduct = "Failed to update product. Please try again.";
    public const string ErrorMessageDeletingProduct = "Failed to delete product. Please try again.";

    public static string GetUnexpectedErrorMessage(string errorMessage = "no context found") =>
        string.Format(UnexpectedError, errorMessage);

    public static string GetErrorLoadingAdminCredentialsMessage(string errorMessage = "no context found") =>
        string.Format(ErrorLoadingAdminCredentials, errorMessage);
}
