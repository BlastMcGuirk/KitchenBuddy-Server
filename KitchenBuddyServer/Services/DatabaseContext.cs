using KitchenBuddyServer.DataObjects;
using Microsoft.EntityFrameworkCore;

namespace KitchenBuddyServer.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RecipeIngredient>().HasKey(table => new {
                table.ItemId,
                table.RecipeId
            });
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<PantryItem> PantryItems { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
