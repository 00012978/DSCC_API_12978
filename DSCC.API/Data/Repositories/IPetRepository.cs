using DSCC.API.Data.Models;

namespace DSCC.API.Data.Repositories;

public interface IPetRepository
{
    Task<IEnumerable<PetResponse>> GetAll();
    Task<PetResponse?> GetById(int id);
    Task<PetResponse> Create(PetRequest pet);
    Task Update(int id, PetRequest pet);
    Task Delete(int id);
}
