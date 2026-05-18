using Microsoft.EntityFrameworkCore;
using YellowBook.API.Models;

namespace YellowBook.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Company>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Companies)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Hospitals", Description = "Medical facilities and healthcare services", CreatedAt = DateTime.UtcNow },
            new Category { Id = 2, Name = "Hotels", Description = "Accommodation and hospitality services", CreatedAt = DateTime.UtcNow },
            new Category { Id = 3, Name = "Schools", Description = "Educational institutions", CreatedAt = DateTime.UtcNow },
            new Category { Id = 4, Name = "Internet Providers", Description = "Internet and communication services", CreatedAt = DateTime.UtcNow },
            new Category { Id = 5, Name = "Government Services", Description = "Government offices and services", CreatedAt = DateTime.UtcNow },
            new Category { Id = 6, Name = "Restaurants", Description = "Food and dining establishments", CreatedAt = DateTime.UtcNow },
            new Category { Id = 7, Name = "Local Businesses", Description = "Various local business services", CreatedAt = DateTime.UtcNow }
        );

        // Seed Companies
        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = 1,
                CompanyName = "Mogadishu General Hospital",
                PhoneNumber = "+252-1-123456",
                Email = "info@mgh.som",
                Address = "Mogadishu, Somalia",
                Website = "https://mgh.som",
                Description = "Leading healthcare provider in Somalia",
                Logo = "",
                CategoryId = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Company
            {
                Id = 2,
                CompanyName = "Peace Hotel",
                PhoneNumber = "+252-1-234567",
                Email = "reservations@peacehotel.som",
                Address = "Downtown Mogadishu",
                Website = "https://peacehotel.som",
                Description = "Luxury accommodation in the heart of Mogadishu",
                Logo = "",
                CategoryId = 2,
                CreatedAt = DateTime.UtcNow
            },
            new Company
            {
                Id = 3,
                CompanyName = "Somali International School",
                PhoneNumber = "+252-1-345678",
                Email = "admissions@sis.som",
                Address = "Airport Road, Mogadishu",
                Website = "https://sis.som",
                Description = "International education for Somali students",
                Logo = "",
                CategoryId = 3,
                CreatedAt = DateTime.UtcNow
            }
        );

        // Seed Admin User
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FullName = "System Administrator",
                Username = "admin",
                Email = "admin@yellowbook.som",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}