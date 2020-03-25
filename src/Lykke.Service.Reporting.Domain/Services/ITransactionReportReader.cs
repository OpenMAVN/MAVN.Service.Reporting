using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.Reporting.Domain.Models;

namespace Lykke.Service.Reporting.Domain.Services
{
    public interface ITransactionReportReader
    {
        Task<TransactionReportResult> GetPaginatedAsync(
            int currentPage, int pageSize,
            DateTime from, DateTime to);
        
        Task<IReadOnlyList<TransactionReport>> GetLimitedAsync(
            DateTime from, DateTime to, int limit);
    }
}
