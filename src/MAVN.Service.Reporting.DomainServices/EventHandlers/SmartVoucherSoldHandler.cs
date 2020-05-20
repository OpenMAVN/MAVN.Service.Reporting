using System.Threading.Tasks;
using MAVN.Numerics;
using MAVN.Service.PartnerManagement.Client;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.SmartVouchers.Client;
using MAVN.Service.SmartVouchers.Contract;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class SmartVoucherSoldHandler : IEventHandler<SmartVoucherSoldEvent>
    {
        private const string TxType = "Smart Voucher";
        private const string VoucherStatus = "Sold";

        private readonly ITransactionReportRepository _reportHelper;
        private readonly IPartnerManagementClient _partnerManagementClient;
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ISmartVouchersClient _smartVouchersClient;

        public SmartVoucherSoldHandler(
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

        public async Task HandleAsync(SmartVoucherSoldEvent message)
        {
            var partner = await _partnerManagementClient.Partners.GetByIdAsync(message.PartnerId);
            var customer =
                (await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(message.CustomerId.ToString(), true, true))?.Profile;
            var campaign = await _smartVouchersClient.CampaignsApi.GetByIdAsync(message.CampaignId);

            await _reportHelper.AddAsync(new TransactionReport
            {
                Amount = Money18.Create(message.Amount),
                Id = message.PaymentRequestId,
                PartnerId = message.PartnerId.ToString(),
                Timestamp = message.Timestamp,
                Vertical = partner.BusinessVertical?.ToString(),
                Info = message.VoucherShortCode,
                Currency = message.Currency,
                TransactionType = TxType,
                SenderEmail = customer?.Email,
                SenderName = $"{customer?.FirstName} {customer?.LastName}",
                CampaignName = campaign?.Name,
                Status = VoucherStatus,
            });
        }
    }
}
