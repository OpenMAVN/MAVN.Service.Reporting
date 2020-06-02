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
        /// <returns></returns>
        [Get("/api/report")]
        Task<PaginatedReportResult> FetchReportAsync([Query] TransactionReportByTimeRequest pagingInfo, [Query(CollectionFormat.Multi)] string[] partnerIds);

        /// <summary>
        /// Csv Report for transactions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Get("/api/report/csv")]
        Task<CsvFileReportResult> FetchReportCsvAsync([Query] FetchReportCsvRequest model);
    }
}
