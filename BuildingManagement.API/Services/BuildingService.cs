using Microsoft.EntityFrameworkCore;
using BuildingManagement.API.Entities;
using BuildingManagement.API.Interfaces;
using BuildingManagement.API.Data;

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

    public Building? GetById(int id)
    {   
        // Returns null if not found
        return _context.Buildings.FirstOrDefault(b => b.Id == id);
    }

    // Create Building Date
    public async Task<Building> Create(Building building)
    {
        // Adds entity
        // Persists to database
        // Returns created object
        _context.Buildings.Add(building);
        await _context.SaveChangesAsync();
        return building;
    }

    // Update Building Date
    public async Task<Building?> Update(int id, Building updatedBuilding)
    {
        var existing = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id);

        if (existing == null)
            return null;

        existing.Name = updatedBuilding.Name;
        existing.Address = updatedBuilding.Address;
        existing.NumberOfUnits = updatedBuilding.NumberOfUnits;

        await _context.SaveChangesAsync();

        return existing;
    }

    // Create Building Date
    public async Task<bool> Delete(int id)
    {
        var existing = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id);

        if (existing == null)
            return false;

        _context.Buildings.Remove(existing);

        await _context.SaveChangesAsync();

        return true;
    }
}