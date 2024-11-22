namespace StockApp.Domain.Entities
{
    public class SalesPerformance
    {
        public decimal TotalSales {  get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue => TotalSales == 0 ? 0 : TotalOrders / TotalOrders;
    }
}
