using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Entities;

namespace QuickBite.Api.Data;

public class QuickBiteDbContext : DbContext
{
    public QuickBiteDbContext(DbContextOptions<QuickBiteDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    public void Seed()
    {
        var restaurantId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        Restaurants.Add(new Restaurant
        {
            Id = restaurantId,
            Name = "Burger House",
            IsOpen = true
        });

        MenuItems.Add(new MenuItem
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            RestaurantId = restaurantId,
            Name = "Classic Burger",
            Price = 80m,
            IsAvailable = true
        });

        SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasMany(order => order.Items)
            .WithOne()
            .HasForeignKey(item => item.OrderId);

        modelBuilder.Entity<Order>()
            .Property(order => order.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(item => item.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<MenuItem>()
            .Property(item => item.Price)
            .HasPrecision(18, 2);
    }
}
