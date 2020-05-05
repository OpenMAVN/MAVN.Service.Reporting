using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.Vouchers.Contract;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class VoucherTokensReservedHandler : IEventHandler<VoucherTokensReservedEvent>
    {
        private const string TxType = "Purchase a voucher";
        private const string ReceiverName = "Retail";

        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ITransactionReportRepository _reportHelper;

        public VoucherTokensReservedHandler(
            ICustomerProfileClient customerProfileClient,
            ITransactionReportRepository reportHelper
        )
        {
            _customerProfileClient = customerProfileClient;
            _reportHelper = reportHelper;
        }

        public async Task HandleAsync(VoucherTokensReservedEvent msg)
        {
            var senderResponse = await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(msg.CustomerId.ToString());
            var sender = senderResponse?.Profile;

            await _reportHelper.AddAsync(
                new TransactionReport
                {
                    Id = msg.TransferId.ToString(),
                    Timestamp = msg.Timestamp,
                    Amount = msg.Amount,
                    TransactionType = TxType,
                    Vertical = Vertical.Retail.ToString(),
                    TransactionCategory = TxCategory.Redemption.ToString(),
                    Status = TxStatus.Reserved.ToString(),
                    SenderName = $"{sender?.FirstName} {sender?.LastName}",
                    SenderEmail = sender?.Email,
                    ReceiverName = ReceiverName,
                }
            );
        }
    }
}
