using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.Reporting.Domain.Models;

namespace MAVN.Service.Reporting.Domain.Services
{
    public interface ITransactionReportReader
    {
        Task<TransactionReportResult> GetPaginatedAsync(
            int currentPage, int pageSize,
            DateTime from, DateTime to, string[] partnerIds, Guid? campaignId,
            string transactionType = null, string status = null);

        Task<IReadOnlyList<TransactionReport>> GetLimitedAsync(
            DateTime from, DateTime to, int limit, string[] partnerIds, Guid? campaignId,
            string transactionType = null, string status = null);
    }
}
