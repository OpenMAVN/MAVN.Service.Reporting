using System;
using System.ComponentModel.DataAnnotations;

namespace MAVN.Service.Reporting.Client.Models
{
    /// <summary>
    /// Holds filtering information for FetchReportCsv request
    /// </summary>
    public class FetchReportCsvRequest
    {
        /// <summary>
        /// From
        /// </summary>
        [Required]
        public DateTime From { get; set; }

        /// <summary>
        /// To
        /// </summary>
        [Required]
        public DateTime To { get; set; }

        /// <summary>
        /// TransactionType
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// CampaingId used for filtering
        /// </summary>
        public Guid? CampaignId { get; set; }

        /// <summary>
        /// Partner Ids used for filtering
        /// </summary>
        public string[] PartnerIds { get; set; }
    }
}
