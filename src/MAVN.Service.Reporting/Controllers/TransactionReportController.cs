using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.Reporting.Client.Api;
using MAVN.Service.Reporting.Client.Models;
using MAVN.Service.Reporting.DomainServices.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.Reporting.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionReportController : ControllerBase, IReportApi
    {
        private readonly ITransactionReportReader _reportReader;
        private readonly IMapper _mapper;

        public TransactionReportController(
            ITransactionReportReader reportReader,
            IMapper mapper)
        {
            _reportReader = reportReader;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of transaction infos
        /// </summary>
        /// <param name="pagingInfo">The paging information</param>
        /// <param name="partnerIds"></param>
        /// <returns></returns>
        [HttpGet("report")]
        [ProducesResponseType(typeof(PaginatedReportResult), (int)HttpStatusCode.OK)]
        public async Task<PaginatedReportResult> FetchReportAsync([FromQuery] TransactionReportByTimeRequest pagingInfo, [FromQuery] string[] partnerIds)
        {
            var result = await _reportReader.GetPaginatedAsync(
                pagingInfo.CurrentPage, pagingInfo.PageSize,
                pagingInfo.From, pagingInfo.To, partnerIds,
                pagingInfo.TransactionType, pagingInfo.Status);

            return _mapper.Map<PaginatedReportResult>(result);
        }

        /// <summary>
        /// Get Csv file of transaction infos
        /// </summary>
        /// <param name="pagingInfo"></param>
        /// <param name="partnerIds"></param>
        /// <returns></returns>
        [HttpGet("report/csv")]
        [ProducesResponseType(typeof(CsvFileReportResult), (int)HttpStatusCode.OK)]
        public async Task<CsvFileReportResult> FetchReportCsvAsync([FromQuery] TransactionReportByTimeRequest pagingInfo, [FromQuery] string[] partnerIds)
        {
            var reports = await _reportReader.GetLimitedAsync(
                pagingInfo.From, pagingInfo.To, Constants.LimitOfReports,
                partnerIds, pagingInfo.TransactionType, pagingInfo.Status);
            var result = CsvConverter.Run(reports);

            return new CsvFileReportResult
            {
                Content = result.ToUtf8Bytes()
            };
        }
    }
}
