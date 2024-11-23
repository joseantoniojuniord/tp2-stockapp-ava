using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class FraudController : ControllerBase
{
    private readonly IFraudDetectionService _fraudDetectionService;

    public FraudController(IFraudDetectionService fraudDetectionService)
    {
        _fraudDetectionService = fraudDetectionService;
    }

    [HttpPost("detect")]
    public async Task<IActionResult> DetectFraud([FromBody] FraudDto fraud)
    {
        if (fraud == null)
            return BadRequest("Invalid fraud data");

        bool isFraud = await _fraudDetectionService.DetectFraudAsync(fraud);
        if (isFraud)
            return Ok("Fraud detected");
        else
            return Ok("No fraud detected");
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessFraud([FromBody] FraudDto fraud)
    {
        if (fraud == null)
            return BadRequest("Invalid fraud data");

        // Process the fraud detection logic
        return Ok("Fraud processed");
    }
}
