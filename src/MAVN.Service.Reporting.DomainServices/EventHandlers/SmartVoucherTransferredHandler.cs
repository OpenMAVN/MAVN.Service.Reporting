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
    public class SmartVoucherTransferredHandler : IEventHandler<SmartVoucherTransferredEvent>
    {
        private const string TxType = "Voucher Transferred";
        private const string VoucherStatus = "Transferred";

        private readonly ITransactionReportRepository _reportHelper;
        private readonly IPartnerManagementClient _partnerManagementClient;
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ISmartVouchersClient _smartVouchersClient;

        public SmartVoucherTransferredHandler(
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

        public async Task HandleAsync(SmartVoucherTransferredEvent message)
        {
            var partner = await _partnerManagementClient.Partners.GetByIdAsync(message.PartnerId);
            var senderCustomer =
                (await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(message.OldCustomerId.ToString(),
                    true, true))?.Profile;
            var receiverCustomer =
                (await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(message.NewCustomerId.ToString(),
                    true, true))?.Profile;
            var campaign = await _smartVouchersClient.CampaignsApi.GetByIdAsync(message.CampaignId);

            await _reportHelper.AddAsync(new TransactionReport
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
                SenderEmail = senderCustomer?.Email,
                SenderName = $"{senderCustomer?.FirstName} {senderCustomer?.LastName}",
                ReceiverEmail = receiverCustomer?.Email,
                ReceiverName = $"{receiverCustomer?.FirstName} {receiverCustomer?.LastName}",
                PartnerName = partner?.Name,
            });
        }
    }
}
