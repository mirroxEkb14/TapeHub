#region Imports
using SQLite;
using TapeHubDemo.Model;
using Location = TapeHubDemo.Model.Location;
#endregion

namespace TapeHubDemo.Database;

//
// Summary:
//     Provides DB-related operations, including DB initialization, table creation, and DB deletion.
public class DatabaseService
{
    private static SQLiteAsyncConnection? _database;
    private const string DatabaseFilename = "TapeHubDemo.db3";

    //
    // Summary:
    //     Checks, if the DB is already initialized.
    //     Defines the path to the DB file.
    //     Initializes the database connection.
    //     Creates tables for all the entities.
    public static async Task InitializeDatabase()
    {
        if (_database != null)
            return;

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        _database = new SQLiteAsyncConnection(databasePath);

        await _database.CreateTableAsync<Product>();    
        await _database.CreateTableAsync<ShopBranch>();
        await _database.CreateTableAsync<User>();
        await _database.CreateTableAsync<ContactInfo>();
        await _database.CreateTableAsync<Location>();

        await LocationService.PopulateLocationsAsync();
        await ContactInfoService.PopulateContactInfosAsync();
        await ShopBranchService.PopulateShopBranchesAsync();
        await ProductService.PopulateProductsAsync();
        await UserService.PopulateAdminsAsync();
    }

    public static SQLiteAsyncConnection GetDatabase() => _database;

    //
    // Summary:
    //     Deletes the database file.
    public static void DeleteDatabase()
    {
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        if (File.Exists(databasePath))
            File.Delete(databasePath);
    }
}
