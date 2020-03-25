using System.Collections.Generic;

namespace Lykke.Service.Reporting.Domain.Models
{
    public class TransactionReportResult
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IReadOnlyList<TransactionReport> TransactionReports { get; set; }
    }
}
