using DSCC.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC.API.Data.Repositories;

public class PetRepository : IPetRepository
{
    private readonly MainDbContext _context;

    public PetRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pet>> GetAll()
    {
        var pets = await _context.Pets
            .Include(a => a.Adoption)
            .ToListAsync();
        return pets;
    }

    public async Task<Pet?> GetById(int id)
    {
        var pet = await _context.Pets
            .Include(a => a.Adoption)
            .FirstOrDefaultAsync(t => t.Id == id);
        return pet;
    }

    public async Task<Pet> Create(Pet pet)
    {
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();
        return pet;
    }

    public async Task Delete(int id)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null)
            throw new Exception($"Pet ID = {id} not found.");

        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
    }


    public async Task Update(int id, Pet pet)
    {
        _context.Entry(pet).State = EntityState.Modified;

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
