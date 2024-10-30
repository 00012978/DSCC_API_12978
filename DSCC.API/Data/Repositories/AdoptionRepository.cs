using DSCC.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC.API.Data.Repositories;

public class AdoptionRepository : IAdoptionRepository
{
    private readonly MainDbContext _context;

    public AdoptionRepository(MainDbContext context)
    {
        _context = context;
    }
    public async Task<Adoption> Create(Adoption adoption)
    {
        _context.Adoptions.Add(adoption);
        await _context.SaveChangesAsync();
        return adoption;
    }

    public async Task Delete(int id)
    {
        var adoption = await _context.Adoptions.FindAsync(id);
        if (adoption == null)
            throw new Exception($"Adoption ID = {id} not found.");

        _context.Adoptions.Remove(adoption);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Adoption>> GetAll()
    {
        var adoptions = await _context.Adoptions
            .Include(a => a.Pet)
            .ToListAsync();
        return adoptions;
    }

    public async Task<Adoption?> GetById(int id)
    {
        var adoption = await _context.Adoptions
            .Include(a => a.Pet)
            .FirstOrDefaultAsync(t => t.Id == id);
        return adoption;
    }

    public async Task Update(int id, Adoption adoption)
    {
        _context.Entry(adoption).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AdoptionExists(id))
            {
                throw new Exception($"Adoption ID = {id} not found.");
            }
            else
            {
                throw;
            }
        }
    }
    private bool AdoptionExists(int id)
    {
        return _context.Adoptions.Any(e => e.Id == id);
    }
}
