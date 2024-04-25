using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zad3App1.Data;
using Zad3App1.Model;

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
    public async Task<IActionResult> GetAnimals()
    {
        var animals = await _context.Animals.ToListAsync();
        return Ok(animals);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAnimal([FromBody] Animal animal)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetAnimals", new { id = animal.Id }, animal);
    }
}
