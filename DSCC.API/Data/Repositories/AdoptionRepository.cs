using DSCC.API.Data.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DSCC.API.Data.Repositories;

public class AdoptionRepository : IAdoptionRepository
{
    private readonly MainDbContext _context;

    public AdoptionRepository(MainDbContext context)
    {
        _context = context;
    }
    public async Task<AdoptionResponse> Create(AdoptionRequest adoption)
    {
        var req = adoption.Adapt<Adoption>();
        var pet = await _context.Pets.FindAsync(adoption.PetId);
        if (pet == null)
        {
            throw new Exception("Pet not found");
        }
        req.Pet = pet;
        _context.Adoptions.Add(req);
        await _context.SaveChangesAsync();

        pet.AdoptionId = req.Id;
        pet.Adoption = req;
        await _context.SaveChangesAsync(); // Save changes to Pet

        var res = req.Adapt<AdoptionResponse>();
        return res;
    }

    public async Task Delete(int id)
    {
        var adoption = await _context.Adoptions.FindAsync(id);
        if (adoption == null)
            throw new Exception($"Adoption ID = {id} not found.");

        _context.Adoptions.Remove(adoption);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AdoptionResponse>> GetAll()
    {
        var adoptions = await _context.Adoptions
            .Include(a => a.Pet)
            .ToListAsync();
        var res = adoptions.Adapt<List<AdoptionResponse>>();
        return res;
    }

    public async Task<AdoptionResponse?> GetById(int id)
    {
        var adoption = await _context.Adoptions
            .Include(a => a.Pet)
            .FirstOrDefaultAsync(t => t.Id == id);
        var res = adoption.Adapt<AdoptionResponse?>();
        return res;
    }

    public async Task Update(int id, AdoptionRequest adoption)
    {
        var req = adoption.Adapt<Adoption>();
        var pet = await _context.Pets.FindAsync(adoption.PetId);
        if (pet == null)
        {
            throw new Exception("Pet not found");
        }
        
        req.Id = id;
        req.Pet = pet;
        _context.Entry(req).State = EntityState.Modified;


        try
        {
            await _context.SaveChangesAsync();

            pet.AdoptionId = req.Id;
            pet.Adoption = req;
            await _context.SaveChangesAsync(); // Save changes to Pet
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
