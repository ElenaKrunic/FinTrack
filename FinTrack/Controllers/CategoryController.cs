using FinTrack.DTOs;
using FinTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{

    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        var categories = await _context.Categories.Include(c => c.Transactions).ToListAsync();

        var categoryDtos = categories.Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();

        return categoryDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    {
        var category = await _context.Categories.Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        var categoryDto = new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        };

        return categoryDto;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> PostCategory([FromBody] CategoryDTO categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = new Category
        {
            Name = categoryDto.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var createdCategoryDto = new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        };

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, createdCategoryDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryDTO categoryDto)
    {
       
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = categoryDto.Name;

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }

}
