using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.Reporting.Client.Models;
using Refit;

namespace MAVN.Service.Reporting.Client.Api
{

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
        /// <param name="partnerIds"></param>
        /// <param name="transactionType">Optional</param>
        /// <param name="status">Optional</param>
        /// <returns></returns>
        [Get("/api/report")]
        Task<PaginatedReportResult> FetchReportAsync(
            [Query] TransactionReportByTimeRequest pagingInfo,
            [Query(CollectionFormat.Multi)] string[] partnerIds,
            [Query] string transactionType = null,
            [Query] string status = null);

        /// <summary>
        /// Csv Report for transactions
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="partnerIds"></param>
        /// <param name="transactionType">Optional</param>
        /// <param name="status">Optional</param>
        /// <returns></returns>
        [Get("/api/report/csv")]
        Task<CsvFileReportResult> FetchReportCsvAsync(
            [Query] [Required] DateTime from,
            [Query] [Required] DateTime to,
            [Query(CollectionFormat.Multi)] string[] partnerIds,
            [Query] string transactionType = null,
            [Query] string status = null);
    }
}
