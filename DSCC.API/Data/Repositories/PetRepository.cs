using DSCC.API.Data.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DSCC.API.Data.Repositories;

public class PetRepository : IPetRepository
{
    private readonly MainDbContext _context;

    public PetRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PetResponse>> GetAll()
    {

        var pets = await _context.Pets
            .Include(a => a.Adoption)
            .ToListAsync();
        var res = pets.Adapt<List<PetResponse>>();
        return res;
    }

    public async Task<PetResponse?> GetById(int id)
    {
        var pet = await _context.Pets
            .Include(a => a.Adoption)
            .FirstOrDefaultAsync(t => t.Id == id);
        var res = pet.Adapt<PetResponse?>();
        return res;
    }

    public async Task<PetResponse> Create(PetRequest pet)
    {
        var req = pet.Adapt<Pet>();
        _context.Pets.Add(req);
        await _context.SaveChangesAsync();
        var res = req.Adapt<PetResponse>();
        return res;
    }

    public async Task Delete(int id)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null)
            throw new Exception($"Pet ID = {id} not found.");

        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
    }


    public async Task Update(int id, PetRequest pet)
    {
        var req = pet.Adapt<Pet>();
        req.Id = id;
        _context.Entry(req).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PetExists(id))
            {
                throw new Exception($"Pet ID = {id} not found.");
            }
            else
            {
                throw;
            }
        }
    }
    private bool PetExists(int id)
    {
        return _context.Pets.Any(e => e.Id == id);
    }
}
