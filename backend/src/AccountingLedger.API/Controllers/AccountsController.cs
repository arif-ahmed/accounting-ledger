using AccountingLedger.Application.Commands;
using AccountingLedger.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingLedger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { AccountId = result });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllAccountsQuery());
            return Ok(result);
        }
    }
}
