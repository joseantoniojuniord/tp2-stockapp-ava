using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesPerformanceAnalysisService _salesPerformanceAnalysisService;

        public SalesController(ISalesPerformanceAnalysisService servicePerformanceAnalysisService)
        {
            _salesPerformanceAnalysisService = servicePerformanceAnalysisService;
        }

        [HttpGet("performance")]
        public async Task<IActionResult> GetSalesPerformance()
        {
            var performance = await _salesPerformanceAnalysisService.AnalyzeSalesPerformanceAsync();
            return Ok(performance);
        }

    }
}
