using System;
using Falcon.Numerics;
using JetBrains.Annotations;

namespace Lykke.Service.Reporting.Domain.Models
{
    public class TransactionReport
    {
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }

        public Money18? Amount { get; set; }

        public string TransactionType { get; set; }

        [CanBeNull] public string Status { get; set; }

        [CanBeNull] public string Vertical { get; set; }

        [CanBeNull] public string TransactionCategory { get; set; }

        [CanBeNull] public string CampaignName { get; set; }

        [CanBeNull] public string Info { get; set; }

        [CanBeNull] public string SenderName { get; set; }
        [CanBeNull] public string SenderEmail { get; set; }

        [CanBeNull] public string ReceiverName { get; set; }
        [CanBeNull] public string ReceiverEmail { get; set; }

        [CanBeNull] public string LocationInfo { get; set; }
        [CanBeNull] public string LocationExternalId { get; set; }
        [CanBeNull] public string LocationIntegrationCode { get; set; }
    }
}
