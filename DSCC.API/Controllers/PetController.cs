using DSCC.API.Data.Models;
using DSCC.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSCC.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
    private readonly IPetRepository _repository;

    public PetController(IPetRepository repository)
    {
        _repository = repository;
    }

    // GET: api/<PetController>
    [HttpGet]
    public async Task<IEnumerable<Pet>> Get()
    {

        return await _repository.GetAll();
    }

    // GET api/<PetController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pet>> Get(int id)
    {
        var result = await _repository.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }

    // POST api/<PetController>
    [HttpPost]
    public async Task<Pet> Post(Pet pet)
    {
        return await _repository.Create(pet);
    }

    // PUT api/<PetController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] Pet pet)
    {
        await _repository.Update(id, pet);
    }

    // DELETE api/<PetController>/5
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
}
