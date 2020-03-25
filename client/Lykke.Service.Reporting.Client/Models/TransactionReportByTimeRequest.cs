using System;
using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.Reporting.Client.Models
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
    }
}
