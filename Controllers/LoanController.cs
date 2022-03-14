using Microsoft.AspNetCore.Mvc;
using MongoDbApi.Models;
using MongoDbApi.Services;

namespace MongoDbApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController: ControllerBase
{
    private readonly LoanService _loanService;

    public LoanController(LoanService loanService)
    {
        _loanService = loanService;
    }
    
    [HttpGet]
    public async Task<List<Loan>> Get() =>
        await _loanService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Loan>> Get(string id)
    {
        var book = await _loanService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Loan newLoan)
    {
        await _loanService.CreateAsync(newLoan);

        return CreatedAtAction(nameof(Get), new { id = newLoan._Id }, newLoan);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Loan updatedLoan)
    {
        var book = await _loanService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedLoan._Id = book._Id;

        await _loanService.UpdateAsync(id, updatedLoan);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _loanService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _loanService.RemoveAsync(id);

        return NoContent();
    }
}