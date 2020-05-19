using System;
using System.ComponentModel.DataAnnotations;

namespace MAVN.Service.Reporting.Client.Models
{
    /// <summary>
    /// Holds pagination information and time period for selection
    /// </summary>
    public class TransactionReportByTimeRequest : PaginationModel
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
    }
}
