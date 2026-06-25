namespace BuildingManagement.API.Interfaces;

using BuildingManagement.API.Entities;

public interface IBuildingService
{
    List<Building> GetAll();
    Building? GetById(int id);
    void Create(Building building);
}

