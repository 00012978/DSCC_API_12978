using DSCC.API.Data.Models;

namespace DSCC.API.Data.Repositories;

public interface IPetRepository
{
    Task<IEnumerable<Pet>> GetAll();
    Task<Pet?> GetById(int id);
    Task<Pet> Create(Pet pet);
    Task Update(int id, Pet pet);
    Task Delete(int id);
}
