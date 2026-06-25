using Microsoft.EntityFrameworkCore;
using BuildingManagement.API.Entities;

namespace BuildingManagement.API.Data;

public class ApplicationDbContext : DbContext
{
    // Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Represents the Buildings table
    public DbSet<Building> Buildings { get; set; }
}