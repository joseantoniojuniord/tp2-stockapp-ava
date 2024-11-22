namespace StockApp.Application.DTOs
{
    public class SalesPerformanceDto
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
    }
}
