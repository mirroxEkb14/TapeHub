using TapeHubDemo.Model;

namespace TapeHubDemo.Database;

public static class ShopBranchService
{
    public static async Task<int> AddShopBranchAsync(ShopBranch shopBranch) =>
    await DatabaseService.GetDatabase().InsertAsync(shopBranch);

    public static async Task<ShopBranch> GetShopBranchByIdAsync(int id) =>
        await DatabaseService.GetDatabase().Table<ShopBranch>().FirstOrDefaultAsync(sb => sb.ID == id);

    public static async Task<List<ShopBranch>> GetAllShopBranchesAsync() =>
        await DatabaseService.GetDatabase().Table<ShopBranch>().ToListAsync();

    public static async Task<int> UpdateShopBranchAsync(ShopBranch shopBranch) =>
        await DatabaseService.GetDatabase().UpdateAsync(shopBranch);

    public static async Task<int> DeleteShopBranchAsync(int id) =>
        await GetShopBranchByIdAsync(id) != null
            ? await DatabaseService.GetDatabase().DeleteAsync<ShopBranch>(id)
            : 0;

    public static async Task PopulateShopBranchesAsync()
    {
        var shopBranches = await GetAllShopBranchesAsync();
        if (shopBranches.Count > 0)
            return;

        var contactInfos = await ContactInfoService.GetAllContactInfosAsync();

        var initialShopBranches = new List<ShopBranch>
        {
            new() { Name = "Pardubice Branch", OpeningHours = "08:00", ClosingHours = "20:00", StockQuantity = 1, ContactInfoID = contactInfos[0].ID, ImagePath = "shopbranch_1.png" },
            new() { Name = "Praha Branch", OpeningHours = "08:00", ClosingHours = "20:00", StockQuantity = 1, ContactInfoID = contactInfos[1].ID, ImagePath = "shopbranch_2.png" },
            new() { Name = "Liberec Branch", OpeningHours = "08:00", ClosingHours = "20:00", StockQuantity = 1, ContactInfoID = contactInfos[2].ID, ImagePath = "shopbranch_3.png" },
            new() { Name = "České Budějovice Branch", OpeningHours = "08:00", ClosingHours = "20:00", StockQuantity = 1, ContactInfoID = contactInfos[3].ID, ImagePath = "shopbranch_4.png" },
            new() { Name = "Ostrava Branch", OpeningHours = "08:00", ClosingHours = "20:00", StockQuantity = 1, ContactInfoID = contactInfos[4].ID, ImagePath = "shopbranch_5.png" }
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
