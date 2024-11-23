namespace StockApp.Application.DTOs
{
    public class FraudDto
    {
        public string TrasactionId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; }

        public string IpAddress { get; set; }

        public string Country { get; set; }
    }
}
