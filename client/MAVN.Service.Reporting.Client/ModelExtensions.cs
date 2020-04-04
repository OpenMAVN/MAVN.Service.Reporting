using System;
using MAVN.Service.Reporting.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.Reporting.Client
{
    /// <summary>
    /// Response model extensions
    /// </summary>
    public static class ModelExtensions
    {
        private const string DefaultFileName = "default.csv";

        /// <summary>
        /// Convert binary data top csv file content
        /// </summary>
        /// <param name="src"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static FileContentResult ToCsvFile(this CsvFileReportResult src, string fileName = DefaultFileName)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            if (string.IsNullOrEmpty(fileName))
                fileName = DefaultFileName;

            return new FileContentResult(src.Content, "text/csv") {FileDownloadName = fileName};
        }
    }
}
