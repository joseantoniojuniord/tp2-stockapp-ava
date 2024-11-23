using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesPerformanceAnalysisService _salesPerformanceAnalysisService;


        public SalesController(ISalesPerformanceAnalysisService salesPerformanceAnalysisService)
        {
            _salesPerformanceAnalysisService = salesPerformanceAnalysisService;
        }

        [HttpGet("performance")]
        [SwaggerOperation(Summary = "Get the performance of sales", Description = "Returns a performance analysis of total sales, average order value, etc.")]
        public async Task<IActionResult> GetSalesPerformance()
        {
       
            var performance = await _salesPerformanceAnalysisService.AnalyzeSalesPerformanceAsync();
            return Ok(performance);
        }
    }
}
