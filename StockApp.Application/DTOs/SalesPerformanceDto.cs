using Swashbuckle.AspNetCore.Annotations;

namespace StockApp.Application.DTOs
{
    public class SalesPerformanceDto
    {
        [SwaggerSchema(Description = "Total sales made")]
        public decimal TotalSales { get; set; }

        [SwaggerSchema(Description = "Total number of orders")]
        public int TotalOrders { get; set; }

        [SwaggerSchema(Description = "Average value of an order")]
        public decimal AverageOrderValue { get; set; }
    }
}
