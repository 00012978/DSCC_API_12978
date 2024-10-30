using DSCC.API.Data.Models;

namespace DSCC.API.Data.Repositories;

public interface IAdoptionRepository
{
    Task<IEnumerable<AdoptionResponse>> GetAll();
    Task<AdoptionResponse?> GetById(int id);
    Task<AdoptionResponse> Create(AdoptionRequest adoption);
    Task Update(int id, AdoptionRequest adoption);
    Task Delete(int id);
}
