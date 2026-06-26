namespace BuildingManagement.API.Interfaces;

using BuildingManagement.API.Entities;
using System.Threading.Tasks;

public interface IBuildingService
{
    IEnumerable<Building> GetAll();
    Building? GetById(int id);
    Task<Building> Create(Building building);
}

