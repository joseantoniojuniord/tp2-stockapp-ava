using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.Application.Services
{
    public class FraudDetectionService : IFraudDetectionService
    {
        private readonly List<string> _blacklistedIps = new() { "192.168.1.1", "10.0.0.5" };
        private readonly TimeSpan _shortTimeWindow = TimeSpan.FromMinutes(5);

   
        public async Task<bool> DetectFraudAsync(FraudDto transaction)
        {
            await Task.Delay(200);

            if (transaction.Amount > 1000)
            {
                return true; 
            }

            if (_blacklistedIps.Contains(transaction.IpAddress))
            {
                return true; 
            }

            if (transaction.Country == "XYZ")
            {
                return true; 
            }

            if (await IsFrequentTransactionAsync(transaction))
            {
                return true; 
            }

            if (!IsTransactionWithinUserHours(transaction))
            {
                return true; 
            }

            return false; 
        }

   
        public async Task<bool> PreventFraudAsync(FraudDto transaction)
        {
            
            if (transaction.Amount > 5000)
            {
                return false; 
            }


            if (_blacklistedIps.Contains(transaction.IpAddress))
            {
                return false; 
            }

     
            if (transaction.Country == "XYZ")
            {
                return false; 
            }

           
            if (!IsTransactionWithinUserHours(transaction))
            {
                return false; 
            }

        
            if (await IsFrequentTransactionAsync(transaction))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> IsFrequentTransactionAsync(FraudDto transaction)
        {
            await Task.Delay(100);

           
            return false; 
        }


        private bool IsTransactionWithinUserHours(FraudDto transaction)
        {
            var startTime = new TimeSpan(8, 0, 0);
            var endTime = new TimeSpan(20, 0, 0);
            var transactioTime = transaction.Date.TimeOfDay;

            return transactioTime >= startTime && transactioTime <= endTime;
        }
    }
}
