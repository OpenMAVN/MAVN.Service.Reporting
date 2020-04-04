using System;
using Falcon.Numerics;
using JetBrains.Annotations;

namespace MAVN.Service.Reporting.Client.Models
{
    /// <summary>
    /// Report for transaction
    /// </summary>
    public class TransactionReport
    {
        /// <summary>Id</summary>
        public Guid Id { get; set; }

        /// <summary>Timestamp</summary>
        public DateTime Timestamp { get; set; }

        /// <summary>Amount</summary>
        public Money18 Amount { get; set; }

        /// <summary>Transaction type</summary>
        public string TransactionType { get; set; }

        /// <summary>Transaction status</summary>
        [CanBeNull] public string Status { get; set; }

        /// <summary>Vertical</summary>
        [CanBeNull] public string Vertical { get; set; }

        /// <summary>Transaction category</summary>
        [CanBeNull] public string TransactionCategory { get; set; }

        /// <summary>Campaign name</summary>
        [CanBeNull] public string CampaignName { get; set; }

        /// <summary>Info</summary>
        [CanBeNull] public string Info { get; set; }

        /// <summary>Sender name</summary>
        [CanBeNull] public string SenderName { get; set; }

        /// <summary>Sender email</summary>
        [CanBeNull] public string SenderEmail { get; set; }

        /// <summary>Receiver name</summary>
        [CanBeNull] public string ReceiverName { get; set; }

        /// <summary>Receiver email</summary>
        [CanBeNull] public string ReceiverEmail { get; set; }

        /// <summary>Location info</summary>
        [CanBeNull] public string LocationInfo { get; set; }

        /// <summary>Location external id</summary>
        [CanBeNull] public string LocationExternalId { get; set; }

        /// <summary>Location integration code</summary>
        [CanBeNull] public string LocationIntegrationCode { get; set; }
    }
}
