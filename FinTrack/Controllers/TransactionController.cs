using FinTrack.DTOs;
using FinTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
    {
        var transactions = await _context.Transactions
            .Include(t => t.Category)
            .Include(t => t.User)
            .ToListAsync();

        var transactionDtos = transactions.Select(t => new TransactionDTO
        {
            Amount = t.Amount,
            Date = t.Date,
            Description = t.Description,
            CategoryId = t.CategoryId,
            UserId = t.UserId
        }).ToList();

        return transactionDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Category)
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null)
        {
            return NotFound();
        }

        var transactionDto = new TransactionDTO
        {
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description,
            CategoryId = transaction.CategoryId,
            UserId = transaction.UserId
        };

        return transactionDto;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction([FromBody] TransactionDTO transactionDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _context.Categories.FindAsync(transactionDto.CategoryId);
        if (category == null)
        {
            return BadRequest("Invalid CategoryId");
        }

        var user = await _context.Users.FindAsync(transactionDto.UserId);
        if (user == null)
        {
            return BadRequest("Invalid UserId");
        }

        var transaction = new Transaction
        {
            Amount = transactionDto.Amount,
            Date = transactionDto.Date,
            Description = transactionDto.Description,
            CategoryId = transactionDto.CategoryId,
            UserId = transactionDto.UserId,
            Category = category, 
            User = user 
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTransaction(int id, TransactionDTO transactionDto)
    {
        if (id != transactionDto.UserId) // Assuming you meant to check the id against the DTO's ID
        {
            return BadRequest();
        }

        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        transaction.Amount = transactionDto.Amount;
        transaction.Date = transactionDto.Date;
        transaction.Description = transactionDto.Description;
        transaction.CategoryId = transactionDto.CategoryId;
        transaction.UserId = transactionDto.UserId;

        _context.Entry(transaction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TransactionExists(id))
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
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TransactionExists(int id)
    {
        return _context.Transactions.Any(e => e.Id == id);
    }
}
