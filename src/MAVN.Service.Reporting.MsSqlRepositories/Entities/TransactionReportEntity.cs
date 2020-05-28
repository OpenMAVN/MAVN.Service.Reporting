using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Numerics;
using JetBrains.Annotations;

namespace MAVN.Service.Reporting.MsSqlRepositories.Entities
{
    [Table("transactions_report_2")]
    public class TransactionReportEntity 
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("amount")]
        public Money18 Amount { get; set; }

        [Column("transaction_type")]
        public string TransactionType { get; set; }

        [Column("status")]
        [CanBeNull]
        public string Status { get; set; }

        [Column("vertical")]
        [CanBeNull]
        public string Vertical { get; set; }

        [Column("transaction_category")]
        [CanBeNull]
        public string TransactionCategory { get; set; }

        [Column("campaign_name")]
        [CanBeNull]
        public string CampaignName { get; set; }

        [Column("info")]
        [CanBeNull]
        public string Info { get; set; }

        [Column("sender_name")]
        [CanBeNull]
        public string SenderName { get; set; }

        [Column("sender_email")]
        [CanBeNull]
        public string SenderEmail { get; set; }

        [Column("receiver_name")]
        [CanBeNull]
        public string ReceiverName { get; set; }

        [Column("receiver_email")]
        [CanBeNull]
        public string ReceiverEmail { get; set; }

        [Column("location_info")]
        [CanBeNull]
        public string LocationInfo { get; set; }

        [Column("location_ext_id")]
        [CanBeNull]
        public string LocationExternalId { get; set; }

        [Column("location_integration_code")]
        [CanBeNull]
        public string LocationIntegrationCode { get; set; }

        [Column("partner_id")]
        [CanBeNull]
        public string PartnerId { get; set; }

        [Column("currency")]
        [CanBeNull]
        public string Currency { get; set; }

        [Column("campaign_id")]
        [CanBeNull] 
        public Guid? CampaignId { get; set; }

        [Column("partner_name")]
        [CanBeNull] 
        public string PartnerName { get; set; }
    }
}
