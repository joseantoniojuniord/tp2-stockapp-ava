using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface ISalesRepository
    {
        Task<List<SalesPerformance>> GetSalesDataAsync();
    }
}
