using Location = TapeHubDemo.Model.Location;

namespace TapeHubDemo.Database;

public static class LocationService
{
    public static async Task<int> AddLocationAsync(Location location) =>
    await DatabaseService.GetDatabase().InsertAsync(location);

    public static async Task<Location> GetLocationByIdAsync(int id) =>
        await DatabaseService.GetDatabase().Table<Location>().FirstOrDefaultAsync(l => l.ID == id);

    public static async Task<List<Location>> GetAllLocationsAsync() =>
        await DatabaseService.GetDatabase().Table<Location>().ToListAsync();

    public static async Task<int> UpdateLocationAsync(Location location) =>
        await DatabaseService.GetDatabase().UpdateAsync(location);

    public static async Task<int> DeleteLocationAsync(int id) =>
        await GetLocationByIdAsync(id) != null
            ? await DatabaseService.GetDatabase().DeleteAsync<Location>(id)
            : 0;

    public static async Task PopulateLocationsAsync()
    {
        var locations = await GetAllLocationsAsync();
        if (locations.Count > 0)
            return;

        var initialLocations = new List<Location>
        {
            new() { Country = "Czech", City = "Pardubice", Street = "Masarykovo náměstí", Number = 1, ZIP = "53002" },
            new() { Country = "Czech", City = "Praha", Street = "Václavské náměstí", Number = 5, ZIP = "11000" },
            new() { Country = "Czech", City = "Liberec", Street = "Rumunská", Number = 13, ZIP = "46001" },
            new() { Country = "Czech", City = "České Budějovice", Street = "Náměstí Přemysla Otakara II", Number = 42, ZIP = "37001" },
            new() { Country = "Czech", City = "Ostrava", Street = "Masarykovo náměstí", Number = 25, ZIP = "70200" },
            new() { Country = "Czech", City = "Praha", Street = "Chemická", Number = 951, ZIP = "14800" }
        };

        foreach (var location in initialLocations)
            await AddLocationAsync(location);
    }
}
