using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Entities;
using PropertyManagement.API.Interfaces;
using PropertyManagement.API.Data;
using PropertyManagement.API.Common;

namespace PropertyManagement.API.Services;

public class BuildingService : IBuildingService
{
    private readonly ILogger<BuildingService> _logger;
    // Private fields go at the TOP of the class, before the constructor
    private readonly ApplicationDbContext _context;

    // Constructor
    public BuildingService(ApplicationDbContext context, ILogger<BuildingService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Result<IEnumerable<Building>> GetAll()
    {   
        _logger.LogInformation("....GET ALL Buildings called....");
        // Pulls all rows from Database
        var buildings =  _context.Buildings.ToList();
        return Result<IEnumerable<Building>>.Success(buildings, "Buildings retrieved successfully");
    }

    // Get Building by id
    public Result<Building> GetById(int id)
    {   
        // Returns null if not found
        var building = _context.Buildings.FirstOrDefault(b => b.Id == id);
        if (building == null)
        {
            return Result<Building>.Failure("Building not found");
        }
        return Result<Building>.Success(building, "Building retrieved successfully");
    }

    // Create Building Date  
    public async Task<Result<Building>> Create(Building building)
    {        
        // var nameExists = await _context.Buildings.AnyAsync(b => b.Name == building.Name);
        //
        // if (nameExists)
        // {
        //     return Result<Building>.Failure("A building with that name already exists.");
        // }
        // Adds entity
        // Persists to database
        // Returns created object
        var validation = await ValidateCreate(building);
        if (!validation.IsSuccess)
        {
            return Result<Building>.Failure(validation.Message);
        }
        
        _context.Buildings.Add(building);
        await _context.SaveChangesAsync();
        //return building;
        return Result<Building>.Success(building, "Building created successfully");
    }

    // Update Building Date
    public async Task<Result<Building>> Update(int id, Building updatedBuilding)
    {
        var existing = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id);
        if (existing == null)
        {
            return Result<Building>.Failure("Building not found");
        }
        
        var nameExists = await _context.Buildings.AnyAsync(b => b.Name == updatedBuilding.Name && b.Id != id);
        if (nameExists)
        {
            return Result<Building>.Failure("A building with that name already exists.");
        }
        existing.Name = updatedBuilding.Name;
        existing.Address = updatedBuilding.Address;
        existing.NumberOfUnits = updatedBuilding.NumberOfUnits;

        await _context.SaveChangesAsync();
        return Result<Building>.Success(existing, "Building updated successfully");
    }

    // Delete Building Date
    public async Task <Result<bool>> Delete(int id)
    {
        var existing = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id);

        if (existing == null)
        {
            return Result<bool>.Failure("Building not found");
        }

        _context.Buildings.Remove(existing);
        await _context.SaveChangesAsync();
        return Result<bool>.Success(true, "Building deleted successfully");
    }
    // Helper ValidateCreate
    private async Task<Result<bool>> ValidateCreate(Building building)
    {
        var nameExists = await _context.Buildings.AnyAsync(
            b => b.Name == building.Name);

        if (nameExists)
        {
            return Result<bool>.Failure("A building with that name already exists.");
        }

        return Result<bool>.Success(true, "Validation passed.");
    }
}