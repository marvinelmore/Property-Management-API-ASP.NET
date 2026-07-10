using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Entities;

namespace PropertyManagement.API.Data;

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