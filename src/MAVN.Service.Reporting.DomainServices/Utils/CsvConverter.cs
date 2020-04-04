using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MAVN.Service.Reporting.Domain.Models;
using CsvHelper;
using System.Globalization;

namespace MAVN.Service.Reporting.DomainServices.Utils
{
    public static class CsvConverter
    {
        public static string Run(IEnumerable<TransactionReport> reports)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                const string delimiter = ",";

                csvWriter.Configuration.Delimiter = delimiter;
                // need intentionally to point out for Excel program what delimiter is used.
                csvWriter.WriteField("sep=" + delimiter, shouldQuote: false);
                csvWriter.NextRecord();

                csvWriter.Configuration.RegisterClassMap<TransactionReportExportMap>();
                csvWriter.Configuration.ShouldQuote = (_, context) =>
                {
                    var propertyIndex = context.Record.Count;

                    return TransactionReportExportMap.Instance.ShouldQuote(propertyIndex);
                };

                csvWriter.Configuration.HasHeaderRecord = true;
                csvWriter.WriteHeader<TransactionReport>();
                csvWriter.NextRecord();

                csvWriter.WriteRecords(reports);

                writer.Flush();

                var result = Encoding.UTF8.GetString(mem.ToArray());

                return result;
            }
        }
    }
}
