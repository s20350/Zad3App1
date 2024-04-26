using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zad3App1.Data;
using Zad3App1.Model;
using System.Linq;

namespace Zad3App1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalController : ControllerBase
{
    private readonly AnimalContext _context;

    public AnimalController(AnimalContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAnimals([FromQuery] string orderBy = "name")
    {
        IQueryable<Animal> query = _context.Animals;

        switch (orderBy.ToLower())
        {
            case "description":
                query = query.OrderBy(a => a.Description);
                break;
            case "category":
                query = query.OrderBy(a => a.Category);
                break;
            case "area":
                query = query.OrderBy(a => a.Area);
                break;
            case "name":
            default:
                query = query.OrderBy(a => a.Name);
                break;
        }

        var animals = await query.ToListAsync();
        return Ok(animals);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAnimal([FromBody] Animal animal)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        var animal = await _context.Animals.FindAsync(id);

        if (animal == null)
        {
            return NotFound();
        }

        return animal;
    }
    
    [HttpPut("{idAnimal}")]
    public async Task<IActionResult> PutAnimal(int idAnimal, [FromBody] Animal animal)
    {
        if (idAnimal != animal.Id)
        {
            return BadRequest();
        }

        _context.Entry(animal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AnimalExists(idAnimal))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    
    [HttpDelete("{idAnimal}")]
    public async Task<IActionResult> DeleteAnimal(int idAnimal)
    {
        var animal = await _context.Animals.FindAsync(idAnimal);
        if (animal == null)
        {
            return NotFound();
        }

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AnimalExists(int id)
    {
        return _context.Animals.Any(e => e.Id == id);
    }
}
