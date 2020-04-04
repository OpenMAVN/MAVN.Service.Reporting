using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.Reporting.Domain.Models;

namespace MAVN.Service.Reporting.Domain.Repositories
{
    public interface ITransactionReportRepository
    {
        Task AddAsync(TransactionReport report);

        Task UpdateStatusAsync(string id, TxStatus status);

        Task<IReadOnlyList<TransactionReport>> GetPaginatedAsync(
            int skip,
            int take,
            DateTime from,
            DateTime to);

        Task<IReadOnlyList<TransactionReport>> GetLimitedAsync(
            DateTime from,
            DateTime to,
            int limit
        );
    }
}
