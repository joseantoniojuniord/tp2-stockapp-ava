using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;

public class SalesPerformanceAnalysisService : ISalesPerformanceAnalysisService
{
    private readonly ISalesRepository _salesRepository;

    public SalesPerformanceAnalysisService(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task<SalesPerformanceDto> AnalyzeSalesPerformanceAsync()
    {
       
        var salesData = await _salesRepository.GetSalesDataAsync(); 

        if (salesData == null || !salesData.Any())
        {
            throw new Exception("No sales data found.");
        }

     
        var totalSales = salesData.Sum(s => s.TotalSales);  
        var totalOrders = salesData.Count; 
        var averageOrderValue = totalOrders > 0 ? totalSales / totalOrders : 0;  

 
        return new SalesPerformanceDto
        {
            TotalSales = totalSales,
            TotalOrders = totalOrders,
            AverageOrderValue = averageOrderValue
        };
    }
}
