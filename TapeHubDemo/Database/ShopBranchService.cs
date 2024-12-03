using TapeHubDemo.Model;
using Location = TapeHubDemo.Model.Location;

namespace TapeHubDemo.Database;

//
// Summary:
//     Provides basic CRUD operations (create, red, update, delete) for the «ShopBranch» model.
public static class ShopBranchService
{
    private const string OpeningHoursPattern = "Opened From: {0}";
    private const string ClosingHoursPattern = "Closed From: {0}";

    public static async Task<int> AddShopBranchAsync(ShopBranch shopBranch) =>
        await DatabaseService.GetDatabase().InsertAsync(shopBranch);

    public static async Task<ShopBranch> GetShopBranchByIdAsync(int id) =>
        await DatabaseService.GetDatabase().Table<ShopBranch>().FirstOrDefaultAsync(sb => sb.ID == id);

    public static async Task<List<ShopBranch>> GetAllShopBranchesAsync() =>
        await DatabaseService.GetDatabase().Table<ShopBranch>().ToListAsync();

    public static async Task<int> UpdateShopBranchAsync(ShopBranch shopBranch) =>
        await DatabaseService.GetDatabase().UpdateAsync(shopBranch);

    //
    // Summary:
    //     Deletes «ShopBranch», also related «Location» and «ContactInfo» objects.
    public static async Task<int> DeleteShopBranchAsync(int id)
    {
        var database = DatabaseService.GetDatabase();

        var shopBranch = await GetShopBranchByIdAsync(id);
        if (shopBranch == null)
            return 0;

        var contactInfo = await ContactInfoService.GetContactInfoByIdAsync(shopBranch.ContactInfoID);
        if (contactInfo != null)
        {
            if (contactInfo.LocationID != null)
            {
                var location = await LocationService.GetLocationByIdAsync((int)contactInfo.LocationID);
                if (location != null)
                    await database.DeleteAsync<Location>(location.ID);
            }
            await database.DeleteAsync<ContactInfo>(contactInfo.ID);
        }
        return await database.DeleteAsync<ShopBranch>(id);
    }

    public static async Task PopulateShopBranchesAsync()
    {
        var shopBranches = await GetAllShopBranchesAsync();
        if (shopBranches.Count > 0)
            return;

        var contactInfos = await ContactInfoService.GetAllContactInfosAsync();

        var initialShopBranches = new List<ShopBranch>
        {
            new() { Name = "Pardubice Branch", OpeningHours = string.Format(OpeningHoursPattern, "08:00"), ClosingHours = string.Format(ClosingHoursPattern, "20:00"), StockQuantity = 6, ContactInfoID = contactInfos[0].ID, ImagePath = "shopbranch_1.png" },
            new() { Name = "Praha Branch", OpeningHours = string.Format(OpeningHoursPattern, "08:00"), ClosingHours = string.Format(ClosingHoursPattern, "20:00"), StockQuantity = 6, ContactInfoID = contactInfos[1].ID, ImagePath = "shopbranch_2.png" },
            new() { Name = "Liberec Branch", OpeningHours = string.Format(OpeningHoursPattern, "08:00"), ClosingHours = string.Format(ClosingHoursPattern, "20:00"), StockQuantity = 6, ContactInfoID = contactInfos[2].ID, ImagePath = "shopbranch_3.png" },
            new() { Name = "České Budějovice Branch", OpeningHours = string.Format(OpeningHoursPattern, "08:00"), ClosingHours = string.Format(ClosingHoursPattern, "20:00"), StockQuantity = 6, ContactInfoID = contactInfos[3].ID, ImagePath = "shopbranch_4.png" },
            new() { Name = "Ostrava Branch", OpeningHours = string.Format(OpeningHoursPattern, "08:00"), ClosingHours = string.Format(ClosingHoursPattern, "20:00"), StockQuantity = 6, ContactInfoID = contactInfos[4].ID, ImagePath = "shopbranch_5.png" }
        };

        foreach (var shopBranch in initialShopBranches)
            await AddShopBranchAsync(shopBranch);
    }

    public static async Task<List<ShopBranch>> GetAllBranchesAsync()
    {
        var db = DatabaseService.GetDatabase();
        return await db.Table<ShopBranch>().ToListAsync();
    }
}
