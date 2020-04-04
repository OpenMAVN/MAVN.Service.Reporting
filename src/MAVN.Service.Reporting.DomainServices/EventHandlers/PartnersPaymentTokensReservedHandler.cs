using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.PartnerManagement.Client;
using Lykke.Service.PartnerManagement.Client.Models.Location;
using Lykke.Service.PartnersPayments.Contract;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class PartnersPaymentTokensReservedHandler : IEventHandler<PartnersPaymentTokensReservedEvent>
    {
        private const string TxType = "Spend in Hotel";

        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly IPartnerManagementClient _partnerManagementClient;
        private readonly ITransactionReportRepository _reportHelper;
        private readonly ILog _log;

        public PartnersPaymentTokensReservedHandler(
            ICustomerProfileClient customerProfileClient,
            IPartnerManagementClient partnerManagementClient,
            ITransactionReportRepository reportHelper,
            ILogFactory logFactory)
        {
            _customerProfileClient = customerProfileClient;
            _partnerManagementClient = partnerManagementClient;
            _reportHelper = reportHelper;
            _log = logFactory.CreateLog(this);
        }

        public async Task HandleAsync(PartnersPaymentTokensReservedEvent msg)
        {
            // TODO: implement deduplication later

            var senderResponse = await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(msg.CustomerId);
            var sender = senderResponse?.Profile;

            string partnerName = null;
            LocationDetailsModel location = null;
            if (!string.IsNullOrWhiteSpace(msg.LocationId))
            {
                if (Guid.TryParse(msg.LocationId, out var locId))
                {
                    var partner = await _partnerManagementClient.Partners.GetByLocationIdAsync(locId);
                    partnerName = partner?.Name;
                    location = partner?.Locations.FirstOrDefault(l => l.Id == locId);
                }
                else
                {
                    _log.Warning($"Couldn't parse location id {msg.LocationId} as GUID");
                }
            }

            await _reportHelper.AddAsync(
                new TransactionReport
                {
                    Id = msg.PaymentRequestId,
                    Timestamp =  msg.Timestamp,
                    TransactionType = TxType,
                    Status = TxStatus.Reserved.ToString(),
                    Vertical = Vertical.Hospitality.ToString(),
                    TransactionCategory = TxCategory.Redemption.ToString(),
                    Amount = msg.Amount,
                    SenderName = $"{sender?.FirstName} {sender?.LastName}",
                    SenderEmail = sender?.Email,
                    ReceiverName = partnerName,
                    LocationInfo = location?.Name,
                    LocationExternalId = location?.ExternalId,
                    LocationIntegrationCode = location?.AccountingIntegrationCode,
                }
            );
        }
    }
}
