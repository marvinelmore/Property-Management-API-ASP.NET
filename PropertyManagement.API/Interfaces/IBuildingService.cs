namespace PropertyManagement.API.Interfaces;

using PropertyManagement.API.Entities;
using System.Threading.Tasks;
using PropertyManagement.API.Common;

public interface IBuildingService
{
    Result<IEnumerable<Building>> GetAll();
    Result<Building> GetById(int id);
    Task<Result<Building>> Create(Building building);
    Task<Result<Building>> Update(int id, Building updatedBuilding);
    Task <Result<bool>> Delete(int id);
}

