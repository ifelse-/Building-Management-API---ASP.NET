using BuildingManagement.API.Entities;
using BuildingManagement.API.Interfaces;
using BuildingManagement.API.Data;

namespace BuildingManagement.API.Services;

public class BuildingService : IBuildingService
{
    // Private fields go at the TOP of the class, before the constructor
    private readonly ApplicationDbContext _context;

    // Constructor
    public BuildingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Building> GetAll()
    {
        // Pulls all rows from Database
        return _context.Buildings.ToList();
    }

    public Building? GetById(int id)
    {   
        // Returns null if not found
        return _context.Buildings.FirstOrDefault(b => b.Id == id);
    }

    public async Task<Building> Create(Building building)
    {
        // Adds entity
        // Persists to database
        // Returns created object
        _context.Buildings.Add(building);
        await _context.SaveChangesAsync();
        return building;
    }
}