using DSCC.API.Data.Models;

namespace DSCC.API.Data.Repositories;

public interface IAdoptionRepository
{
    Task<IEnumerable<Adoption>> GetAll();
    Task<Adoption?> GetById(int id);
    Task<Adoption> Create(Adoption adoption);
    Task Update(int id, Adoption adoption);
    Task Delete(int id);
}
