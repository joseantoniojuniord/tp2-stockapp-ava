using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface ISalesPerformanceAnalysisService
    {
        Task<SalesPerformanceDto> AnalyzeSalesPerformanceAsync();
    }
}
