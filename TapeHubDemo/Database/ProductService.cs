using TapeHubDemo.Enumeration;
using TapeHubDemo.Model;

namespace TapeHubDemo.Database;

public static class ProductService
{
    public static async Task<int> AddProductAsync(Product product) =>
    await DatabaseService.GetDatabase().InsertAsync(product);

    public static async Task<Product> GetProductByIdAsync(int id) =>
        await DatabaseService.GetDatabase().Table<Product>().FirstOrDefaultAsync(p => p.ID == id);

    public static async Task<List<Product>> GetAllProductsAsync() =>
        await DatabaseService.GetDatabase().Table<Product>().ToListAsync();

    public static async Task<int> UpdateProductAsync(Product product) =>
        await DatabaseService.GetDatabase().UpdateAsync(product);

    public static async Task<int> DeleteProductAsync(int id) =>
        await GetProductByIdAsync(id) != null
            ? await DatabaseService.GetDatabase().DeleteAsync<Product>(id)
            : 0;

    public static async Task PopulateProductsAsync()
    {
        var products = await GetAllProductsAsync();
        if (products.Count > 0)
            return;

        var shopBranches = await ShopBranchService.GetAllShopBranchesAsync();

        var initialProducts = new List<Product>
        {
            new() { Title = "8 MM", Description = "A psychological thriller film", Price = 1.99, Type = ProductType.VHS, ShopBranchID = shopBranches[0].ID, ImagePath = "product_vhs_8_mm.png" },
            new() { Title = "Apocalypse Now", Description = "A war drama film", Price = 2.99, Type = ProductType.VHS, ShopBranchID = shopBranches[1].ID, ImagePath = "product_vhs_apocalypse_now.png" },
            new() { Title = "Demolition Man", Description = "A sci-fi action film", Price = 3.99, Type = ProductType.VHS, ShopBranchID = shopBranches[2].ID, ImagePath = "product_vhs_demolition_man.png" },
            new() { Title = "Gattaca", Description = "A dystopian sci-fi thriller", Price = 4.99, Type = ProductType.VHS, ShopBranchID = shopBranches[3].ID, ImagePath = "product_vhs_gattaca.png" },
            new() { Title = "Primal Fear", Description = "A crime-thriller legal drama", Price = 5.99, Type = ProductType.VHS, ShopBranchID = shopBranches[4].ID, ImagePath = "product_vhs_primal_fear.png" },

            new() { Title = "Ave Maria", Description = "A classic Christmas song", Price = 1.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[0].ID, ImagePath = "audio_cassette_ave_maria.png" },
            new() { Title = "Good King Wenceslas", Description = "A traditional Christmas carol", Price = 2.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[1].ID, ImagePath = "audio_cassette_good_king_wenceslas.png" },
            new() { Title = "I Saw Three Ships", Description = "A popular Christmas carol", Price = 3.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[2].ID, ImagePath = "audio_cassette_i_saw_three_ships.png" },
            new() { Title = "The Boar's Head Carol", Description = "A Christmas carol of medieval origin", Price = 4.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[3].ID, ImagePath = "audio_cassette_the_boars_head_carol.png" },
            new() { Title = "Twelve Days of Christmas", Description = "A famous Christmas song", Price = 5.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[4].ID, ImagePath = "audio_cassette_twelve_days_of_christmas.png" }
        };

        foreach (var product in initialProducts)
            await AddProductAsync(product);
    }

    public static async Task<List<Product>> GetProductsByBranchIdAsync(int branchId)
    {
        var db = DatabaseService.GetDatabase();
        return await db.Table<Product>().Where(p => p.ShopBranchID == branchId).ToListAsync();
    }
}
