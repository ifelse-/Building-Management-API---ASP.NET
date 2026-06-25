using BuildingManagement.API.Entities;
using BuildingManagement.API.Interfaces;

namespace BuildingManagement.API.Services;

public class BuildingService : IBuildingService
{
    // This is a private field — an in-memory list acting as fake database for now
    private readonly List<Building> _buildings = new();

    public List<Building> GetAll()
        {
            return _buildings;
        }

    public Building? GetById(int id)
        {
            return _buildings.FirstOrDefault(x => x.Id == id);
        }

    public void Create(Building building)
        {
            building.Id = _buildings.Count + 1;
            _buildings.Add(building);
        }
}