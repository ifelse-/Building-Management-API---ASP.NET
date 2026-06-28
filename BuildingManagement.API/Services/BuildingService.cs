using Microsoft.EntityFrameworkCore;
using BuildingManagement.API.Entities;
using BuildingManagement.API.Interfaces;
using BuildingManagement.API.Data;
using BuildingManagement.API.Common;

namespace BuildingManagement.API.Services;

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

    public IEnumerable<Building> GetAll()
    {   
        _logger.LogInformation("....GET ALL Buildings called....");
        // Pulls all rows from Database
        return _context.Buildings.ToList();
    }

    // Get Building by Id
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
        // Adds entity
        // Persists to database
        // Returns created object
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
}