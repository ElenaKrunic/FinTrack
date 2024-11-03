using FinTrack.DTOs;
using FinTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            User = user,
            TransactionType = transactionDto.TransactionType
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTransaction(int id, TransactionDTO transactionDto)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        if (transactionDto.Amount != default)
        {
            transaction.Amount = transactionDto.Amount;
        }
        if (transactionDto.Date != default(DateTime))
        {
            transaction.Date = transactionDto.Date;
        }
        if (!string.IsNullOrEmpty(transactionDto.Description))
        {
            transaction.Description = transactionDto.Description;
        }
        if (transactionDto.CategoryId != default)
        {
            transaction.CategoryId = transactionDto.CategoryId;
        }
        if (transactionDto.UserId != default)
        {
            transaction.UserId = transactionDto.UserId;
        }
        if (transactionDto.TransactionType != default)
        {
            transaction.TransactionType = transactionDto.TransactionType;
        }

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

    [HttpGet("totalIncome/{userId}")]
    public async Task<ActionResult<decimal>> GetTotalIncome(int userId)
    {
        var totalIncome = await _context.Transactions
            .Where(t => t.TransactionType == TransactionType.Income && t.UserId == userId)
            .SumAsync(t => t.Amount);

        return Ok(totalIncome);
    }

    [HttpGet("totalExpenses/{userId}")]
    public async Task<ActionResult<decimal>> GetTotalExpenses(int userId)
    {
        var totalExpenses = await _context.Transactions
            .Where(t => t.TransactionType == TransactionType.Expense && t.UserId == userId)
            .SumAsync(t => t.Amount);

        return Ok(totalExpenses);
    }

    private bool TransactionExists(int id)
    {
        return _context.Transactions.Any(e => e.Id == id);
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<TransactionDTO>>> FilterTransactions(int userId, int? categoryId = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Transactions
            .Include(t => t.Category)
            .Include(t => t.User)
            .Where(t => t.UserId == userId)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(t => t.CategoryId == categoryId.Value);
        }

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(t => t.Date >= startDate.Value && t.Date <= endDate.Value);
        }
        else if (startDate.HasValue)
        {
            query = query.Where(t => t.Date >= startDate.Value);
        }
        else if (endDate.HasValue)
        {
            query = query.Where(t => t.Date <= endDate.Value);
        }

        var transactions = await query.ToListAsync();

        var transactionDtos = transactions.Select(t => new TransactionDTO
        {
            Amount = t.Amount,
            Date = t.Date,
            Description = t.Description,
            CategoryId = t.CategoryId,
            UserId = t.UserId,
            TransactionType = t.TransactionType
        }).ToList();

        return Ok(transactionDtos);
    }
}
