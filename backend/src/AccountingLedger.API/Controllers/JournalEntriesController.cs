using AccountingLedger.Application.Commands;
using AccountingLedger.Application.Queries;
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
        var result = await _mediator.Send(new GetAllJournalEntriesQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateJournalEntryCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { JournalEntryId = result });
    }
}
