using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MAVN.Service.Reporting.Domain.Models
{
    public sealed class TransactionReportExportMap : ClassMap<TransactionReport>
    {
        private readonly Dictionary<int, string> QuotedProperties = new Dictionary<int, string>();

        public TransactionReportExportMap()
        {
            var i = 0;

            Map(m => m.Id).Index(i);
            i++;
            Map(m => m.SenderName).Name("Sender Name").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.SenderName));
            i++;
            Map(m => m.SenderEmail).Name("Sender Email").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.SenderEmail));
            i++;
            Map(m => m.Timestamp).Index(i);
            i++;
            Map(m => m.TransactionType).Name("Transaction Type").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.TransactionType));
            i++;
            Map(m => m.Status).Name("Status of Transaction").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.Status));
            i++;
            Map(m => m.CampaignName).Name("Action Rule Name").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.CampaignName));
            i++;
            Map(m => m.Info).Name("Additional Info").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.Info));
            i++;
            Map(m => m.ReceiverName).Name("Receiver Name").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.ReceiverName));
            i++;
            Map(m => m.ReceiverEmail).Name("Receiver Email").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.ReceiverEmail));
            i++;
            Map(m => m.LocationInfo).Name("Transaction Location").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.LocationInfo));
            i++;
            Map(m => m.LocationExternalId).Name("Location External Id").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.LocationExternalId));
            i++;
            Map(m => m.LocationIntegrationCode).Name("Location Integration Code").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.LocationIntegrationCode));
            i++;
            Map(m => m.Vertical).Name("Business Unit").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.Vertical));
            i++;
            Map(m => m.Amount).Index(i);
            i++;
            Map(m => m.TransactionCategory).Name("Transaction Category").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.TransactionCategory));
            i++;
            Map(m => m.PartnerId).Name("Partner Id").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.PartnerId));
            i++;
            Map(m => m.PartnerName).Name("Partner Name").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.PartnerName));
            i++;
            Map(m => m.Currency).Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.Currency));
            i++;
            Map(m => m.CampaignId).Name("Campaign Id").Index(i);
            QuotedProperties.TryAdd(i, nameof(TransactionReport.CampaignId));
        }

        public bool ShouldQuote(int propertyIndex)
        {
            return QuotedProperties.ContainsKey(propertyIndex);
        }

        public static TransactionReportExportMap Instance
        {
            get
            {
                if (_transactionReportExportMap == null)
                    _transactionReportExportMap = new TransactionReportExportMap();

                return _transactionReportExportMap;
            }
        }
        private static TransactionReportExportMap _transactionReportExportMap;
    }
}
