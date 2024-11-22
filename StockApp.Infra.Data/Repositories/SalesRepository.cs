using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Infra.Data.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        public async Task<List<SalesPerformance>> GetSalesDataAsync()
        {
            return await Task.FromResult(new List<SalesPerformance>
            {
                new SalesPerformance { TotalSales = 5000, TotalOrders = 100 },
                new SalesPerformance { TotalSales = 5000, TotalOrders = 100 }
            });
        }
    }

}