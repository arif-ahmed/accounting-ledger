using AccountingLedger.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingLedger.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JournalEntriesController : ControllerBase
{
    private readonly IMediator _mediator;
    public JournalEntriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // Placeholder for actual query handling
        await Task.FromResult(0);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateJournalEntryCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { JournalEntryId = result });
    }
}
