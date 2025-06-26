using AccountingLedger.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingLedger.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrialBalanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrialBalanceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? accountNameFilter,
        [FromQuery] string? sortBy,
        [FromQuery] string? sortOrder,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
                
    {
        var result = await _mediator.Send(new GetTrialBalanceQuery());
        return Ok(result);
    }
}
