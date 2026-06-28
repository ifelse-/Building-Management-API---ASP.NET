namespace BuildingManagement.API.Interfaces;

using BuildingManagement.API.Entities;
using System.Threading.Tasks;
using BuildingManagement.API.Common;

public interface IBuildingService
{
    IEnumerable<Building> GetAll();
    Result<Building> GetById(int id);
    Task<Result<Building>> Create(Building building);
    Task<Result<Building>> Update(int id, Building updatedBuilding);
    Task <Result<bool>> Delete(int id);
}

