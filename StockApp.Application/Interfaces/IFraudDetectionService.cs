using StockApp.Application.DTOs;
using System.Diagnostics;

namespace StockApp.Application.Interfaces
{
    public interface IFraudDetectionService
    {
        Task<bool> DetectFraudAsync(FraudDto transaction);
        Task<bool> PreventFraudAsync(FraudDto transaction);
    }
}
