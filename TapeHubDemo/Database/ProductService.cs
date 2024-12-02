using TapeHubDemo.Enumeration;
using TapeHubDemo.Model;

namespace TapeHubDemo.Database;

//
// Summary:
//     Provides basic CRUD operations (create, red, update, delete) for the «Product» model.
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
            new() { Title = "Interstellar", Description = "An epic science fiction drama film", Price = 2.99, Type = ProductType.VHS, ShopBranchID = shopBranches[0].ID, ImagePath = "product_vhs_interstellar.png" },
            new() { Title = "Chernobyl", Description = "A historical drama film", Price = 2.99, Type = ProductType.VHS, ShopBranchID = shopBranches[0].ID, ImagePath = "product_hvs_chernobyl.png" },
            new() { Title = "Apocalypse Now", Description = "A war drama film", Price = 3.99, Type = ProductType.VHS, ShopBranchID = shopBranches[1].ID, ImagePath = "product_vhs_apocalypse_now.png" },
            new() { Title = "Better Call Saul", Description = "A crime drama film", Price = 4.99, Type = ProductType.VHS, ShopBranchID = shopBranches[1].ID, ImagePath = "product_vhs_better_call_saul.png" },
            new() { Title = "Requiem for a Dream", Description = "A psychological drama film", Price = 4.99, Type = ProductType.VHS, ShopBranchID = shopBranches[1].ID, ImagePath = "product_vhs_requiem_for_a_dream.png" },
            new() { Title = "Demolition Man", Description = "A sci-fi action film", Price = 5.99, Type = ProductType.VHS, ShopBranchID = shopBranches[2].ID, ImagePath = "product_vhs_demolition_man.png" },
            new() { Title = "Boardwalk Empire", Description = "A crime drama film", Price = 6.99, Type = ProductType.VHS, ShopBranchID = shopBranches[2].ID, ImagePath = "product_vhs_boardwalk_empire.png" },
            new() { Title = "Carlito's Way", Description = "A crime drama film", Price = 6.99, Type = ProductType.VHS, ShopBranchID = shopBranches[2].ID, ImagePath = "product_vhs_carlitos_way.png" },
            new() { Title = "Gattaca", Description = "A dystopian sci-fi thriller", Price = 7.99, Type = ProductType.VHS, ShopBranchID = shopBranches[3].ID, ImagePath = "product_vhs_gattaca.png" },
            new() { Title = "Gone Girl", Description = "A mystery crime drama", Price = 8.99, Type = ProductType.VHS, ShopBranchID = shopBranches[3].ID, ImagePath = "product_vhs_gone_girl.png" },
            new() { Title = "I Am Legend", Description = "A zombie action thriller", Price = 8.99, Type = ProductType.VHS, ShopBranchID = shopBranches[3].ID, ImagePath = "product_vhs_i_am_legend.png" },
            new() { Title = "Primal Fear", Description = "A crime-thriller legal drama", Price = 9.99, Type = ProductType.VHS, ShopBranchID = shopBranches[4].ID, ImagePath = "product_vhs_primal_fear.png" },
            new() { Title = "Knockin' On Heaven's Door", Description = "A crime tragicomedy film", Price = 10.99, Type = ProductType.VHS, ShopBranchID = shopBranches[4].ID, ImagePath = "product_vhs_knockin_on_heavens_door.png" },
            new() { Title = "The Silence of the Lambs", Description = "A psychological horror thriller", Price = 10.99, Type = ProductType.VHS, ShopBranchID = shopBranches[4].ID, ImagePath = "product_vhs_the_silence_of_the_lambs.png" },

            new() { Title = "Ave Maria", Description = "A classic Christmas song", Price = 1.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[0].ID, ImagePath = "audio_cassette_ave_maria.png" },
            new() { Title = "The Little Drummer Boy", Description = "A popular Christmas song", Price = 2.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[0].ID, ImagePath = "audio_cassette_the_little_drummer_boy.png" },
            new() { Title = "Theres No Place Like Home", Description = "A Christmas song", Price = 2.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[0].ID, ImagePath = "audio_cassette_theres_no_place_like_home_for_the_holidays.png" },
            new() { Title = "Good King Wenceslas", Description = "A traditional Christmas carol", Price = 3.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[1].ID, ImagePath = "audio_cassette_good_king_wenceslas.png" },
            new() { Title = "Fum, Fum, Fum!", Description = "A traditional Catalan Christmas carol", Price = 4.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[1].ID, ImagePath = "audio_cassette_fum_fum_fum.png" },
            new() { Title = "Tempus Est Iocundum", Description = "A folk neo-classical song", Price = 4.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[1].ID, ImagePath = "audio_cassette_tempus_est_iocundum.png" },
            new() { Title = "I Saw Three Ships", Description = "A popular Christmas carol", Price = 5.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[2].ID, ImagePath = "audio_cassette_i_saw_three_ships.png" },
            new() { Title = "Traust Heilung", Description = "A folk neo-classical song", Price = 6.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[2].ID, ImagePath = "audio_cassette_traust_heilung.png" },
            new() { Title = "Anoana Heilung", Description = "A folk neo-classical song", Price = 6.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[2].ID, ImagePath = "audio_cassette_anoana.png" },
            new() { Title = "The Boar's Head Carol", Description = "A Christmas carol of medieval origin", Price = 7.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[3].ID, ImagePath = "audio_cassette_the_boars_head_carol.png" },
            new() { Title = "Helheim", Description = "A folk neo-classical song", Price = 8.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[3].ID, ImagePath = "audio_cassette_helheim.png" },
            new() { Title = "Shape of My Heart", Description = "A rock-pop song", Price = 8.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[3].ID, ImagePath = "audio_cassette_shape_of_my_heart.png" },
            new() { Title = "Twelve Days of Christmas", Description = "A famous Christmas song", Price = 9.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[4].ID, ImagePath = "audio_cassette_twelve_days_of_christmas.png" },
            new() { Title = "Ai vist lo lop", Description = "An old Occitan folk song", Price = 10.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[4].ID, ImagePath = "audio_cassette_ai_vist_lo_lop.png" },
            new() { Title = "Brave Son of America", Description = "A power pop song", Price = 10.99, Type = ProductType.AudioCassette, ShopBranchID = shopBranches[4].ID, ImagePath = "audio_cassette_brave_son_of_america.png" }
        };

        foreach (var product in initialProducts)
            await AddProductAsync(product);
    }

    //
    // Summary:
    //     Retrieves all products associated with a specific shop branch by branch ID.
    public static async Task<List<Product>> GetProductsByBranchIdAsync(int branchId)
    {
        var db = DatabaseService.GetDatabase();
        return await db.Table<Product>().Where(p => p.ShopBranchID == branchId).ToListAsync();
    }
}
