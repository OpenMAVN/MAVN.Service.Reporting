using JetBrains.Annotations;

namespace Lykke.Service.Reporting.Client.Models
{
    /// <summary>
    /// The csv file result model
    /// </summary>
    [PublicAPI]
    public class CsvFileReportResult
    {
        /// <summary>
        /// Binary file content
        /// </summary>
        public byte[] Content { get; set; } 
    }
}
