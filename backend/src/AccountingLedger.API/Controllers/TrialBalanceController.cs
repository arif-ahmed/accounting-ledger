using Microsoft.AspNetCore.Mvc;

namespace AccountingLedger.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrialBalanceController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.Delay(1000);
        return Ok(new { Message = "Trial Balance retrieved successfully." });
    }
}
