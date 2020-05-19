using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.Reporting.DomainServices.Utils;

namespace MAVN.Service.Reporting.DomainServices.Services
{
    public class TransactionReportReader : ITransactionReportReader
    {
        private readonly ITransactionReportRepository _transactionReportRepository;

        public TransactionReportReader(ITransactionReportRepository transactionReportRepository)
        {
            _transactionReportRepository = transactionReportRepository;
        }

        public async Task<TransactionReportResult> GetPaginatedAsync(
            int currentPage, int pageSize,
            DateTime from, DateTime to, string[] partnerIds,
            string transactionType = null, string status = null)
        {
            var (skip, take) = PagingUtils.GetNextPageParameters(currentPage, pageSize);

            var reports = await _transactionReportRepository.GetPaginatedAsync(skip, take, from, to, partnerIds, transactionType, status);

            return new TransactionReportResult
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = reports.Count,
                TransactionReports = reports
            };
        }

        public async Task<IReadOnlyList<TransactionReport>> GetLimitedAsync(
            DateTime from, DateTime to, int limit, string[] partnerIds,
            string transactionType = null, string status = null)
        {
            if (limit <= 0) throw new ArgumentException();

            var reports = await _transactionReportRepository.GetLimitedAsync(from, to, limit, partnerIds, transactionType, status);
          
            return reports;
        }
    }
}
