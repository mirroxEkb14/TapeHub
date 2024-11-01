using TapeHubDemo.Enumeration;
using TapeHubDemo.Model;

namespace TapeHubDemo.Database;

//
// Summary:
//     Provides basic CRUD operations (create, red, update, delete) for the «User» model.
public static class UserService
{
    public static async Task<int> AddUserAsync(User user)
    {
        var db = DatabaseService.GetDatabase();

        if (db == null)
            return 0;

        return user == null
            ? throw new ArgumentNullException(nameof(user))
            : await db.InsertAsync(user);
    }

    public static async Task<User> GetUserByIdAsync(int id) =>
        await DatabaseService.GetDatabase().Table<User>().FirstOrDefaultAsync(u => u.ID == id);

    public static async Task<List<User>> GetAllUsersAsync() =>
        await DatabaseService.GetDatabase().Table<User>().ToListAsync();

    public static async Task<int> UpdateUserAsync(User user) =>
        await DatabaseService.GetDatabase().UpdateAsync(user);

    public static async Task<int> DeleteUserAsync(int id) =>
        await GetUserByIdAsync(id) != null
            ? await DatabaseService.GetDatabase().DeleteAsync<User>(id)
            : 0;

    public static async Task PopulateAdminsAsync()
    {
        var users = await GetAllUsersAsync();
        if (users.Count > 0)
            return;

        var contactInfos = await ContactInfoService.GetAllContactInfosAsync();

        var initialAdminUsers = new List<User>
        {
            new() { Username = "vance7187", Password = "19Tapehub!19kz", Role = UserRole.Admin, ContactInfoID = contactInfos[5].ID }
        };

        foreach (var adminUser in initialAdminUsers)
            await AddUserAsync(adminUser);
    }

    //
    // Summary:
    //     Retrieves a user from the DB by matching the provided username and password.
    public static async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
    {
        return await DatabaseService.GetDatabase()
            .Table<User>()
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }
}
