using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.Reporting.Client.Models;
using Refit;

namespace MAVN.Service.Reporting.Client.Api {

    /// <summary>
    /// Report API interface.
    /// </summary>
    [PublicAPI]
    public interface IReportApi
    {
        /// <summary>
        /// Report for transactions
        /// </summary>
        /// <param name="pagingInfo"></param>
        /// <returns></returns>
        [Get("/api/report")]
        Task<PaginatedReportResult> FetchReportAsync([Query] TransactionReportByTimeRequest pagingInfo);

        /// <summary>
        /// Csv Report for transactions
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [Get("/api/report/csv")]
        Task<CsvFileReportResult> FetchReportCsvAsync([Query] [Required] DateTime from, [Query] [Required] DateTime to);
    }
}
