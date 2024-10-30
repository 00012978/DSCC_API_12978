using DSCC.API.Data;
using DSCC.API.Data.Models;
using DSCC.API.Data.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSCC.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdoptionController : ControllerBase
{
    private readonly IAdoptionRepository _repository;

    public AdoptionController(IAdoptionRepository repository)
    {
        _repository = repository;
    }
    // GET: api/<AdoptionController>
    [HttpGet]
    public async Task<IEnumerable<AdoptionResponse>> Get()
    {
        var adoptions = await _repository.GetAll();
        var result = adoptions.Adapt <IEnumerable<AdoptionResponse>>();
        return result;
    }

    // GET api/<AdoptionController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AdoptionResponse>> Get(int id)
    {
        var result = await _repository.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }

    // POST api/<AdoptionController>
    [HttpPost]
    public async Task<AdoptionResponse> Post(AdoptionRequest adoption)
    {
        return await _repository.Create(adoption);
    }

    // PUT api/<AdoptionController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] AdoptionRequest adoption)
    {
        await _repository.Update(id, adoption);
    }

    // DELETE api/<AdoptionController>/5
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
}
