using TapeHubDemo.Model;

namespace TapeHubDemo.Database;

public static class ContactInfoService
{
    public static async Task<int> AddContactInfoAsync(ContactInfo contactInfo) =>
    await DatabaseService.GetDatabase().InsertAsync(contactInfo);

    public static async Task<ContactInfo> GetContactInfoByIdAsync(int id) =>
        await DatabaseService.GetDatabase().Table<ContactInfo>().FirstOrDefaultAsync(ci => ci.ID == id);

    public static async Task<List<ContactInfo>> GetAllContactInfosAsync() =>
        await DatabaseService.GetDatabase().Table<ContactInfo>().ToListAsync();

    public static async Task<int> UpdateContactInfoAsync(ContactInfo contactInfo) =>
        await DatabaseService.GetDatabase().UpdateAsync(contactInfo);

    public static async Task<int> DeleteContactInfoAsync(int id) =>
        await GetContactInfoByIdAsync(id) != null
            ? await DatabaseService.GetDatabase().DeleteAsync<ContactInfo>(id)
            : 0;

    public static async Task PopulateContactInfosAsync()
    {
        var contactInfos = await GetAllContactInfosAsync();
        if (contactInfos.Count > 0)
            return;

        var locations = await LocationService.GetAllLocationsAsync();

        var initialContactInfos = new List<ContactInfo>
        {
            new() { Email = "pardubice@tapehub.cz", Phone = "+420 466 123 456", LocationID = locations[0].ID },
            new() { Email = "praha@tapehub.cz", Phone = "+420 224 123 456", LocationID = locations[1].ID },
            new() { Email = "liberec@tapehub.cz", Phone = "+420 485 123 456", LocationID = locations[2].ID },
            new() { Email = "ceskebudejovice@tapehub.cz", Phone = "+420 387 123 456", LocationID = locations[3].ID },
            new() { Email = "ostrava@tapehub.cz", Phone = "+420 596 123 456", LocationID = locations[4].ID },
            new() { Email = "vance7187@tapehub.cz", Phone = "+420 123 456 789", LocationID = locations[5].ID }
        };

        foreach (var contactInfo in initialContactInfos)
            await AddContactInfoAsync(contactInfo);
    }
}
