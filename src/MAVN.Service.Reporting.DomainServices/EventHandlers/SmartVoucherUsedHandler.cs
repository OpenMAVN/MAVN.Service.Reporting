using System;
using System.Threading.Tasks;
using MAVN.Numerics;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.PartnerManagement.Client;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.SmartVouchers.Client;
using MAVN.Service.SmartVouchers.Contract;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class SmartVoucherUsedHandler : IEventHandler<SmartVoucherUsedEvent>
    {
        private const string TxType = "Voucher Redeemed";
        private const string VoucherStatus = "Used";

        private readonly ITransactionReportRepository _reportHelper;
        public readonly IPartnerManagementClient _partnerManagementClient;
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ISmartVouchersClient _smartVouchersClient;

        public SmartVoucherUsedHandler(
            ITransactionReportRepository reportHelper, 
            IPartnerManagementClient partnerManagementClient,
            ICustomerProfileClient customerProfileClient, 
            ISmartVouchersClient smartVouchersClient)
        {
            _reportHelper = reportHelper;
            _partnerManagementClient = partnerManagementClient;
            _customerProfileClient = customerProfileClient;
            _smartVouchersClient = smartVouchersClient;
        }

        public async Task HandleAsync(SmartVoucherUsedEvent message)
        {
            var partner = await _partnerManagementClient.Partners.GetByIdAsync(message.PartnerId);
            var customer =
                (await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(message.CustomerId.ToString(), true, true))?.Profile;
            var campaign = await _smartVouchersClient.CampaignsApi.GetByIdAsync(message.CampaignId);

            await _reportHelper.AddAsync(new TransactionReport()
            {
                Amount = Money18.Create(message.Amount),
                Id = Guid.NewGuid().ToString(),
                PartnerId = message.PartnerId.ToString(),
                Timestamp = message.Timestamp,
                Currency = message.Currency,
                TransactionType = TxType,
                CampaignName = campaign?.Name,
                Status = VoucherStatus,
                CampaignId = message.CampaignId,
                Vertical = partner.BusinessVertical?.ToString(),
                SenderEmail = customer?.Email,
                SenderName = $"{customer?.FirstName} {customer?.LastName}",
                PartnerName = partner?.Name,
            });
        }
    }
}
