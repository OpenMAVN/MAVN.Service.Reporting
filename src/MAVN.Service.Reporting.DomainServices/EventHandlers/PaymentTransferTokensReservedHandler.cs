using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.PaymentTransfers.Contract;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class PaymentTransferTokensReservedHandler : IEventHandler<PaymentTransferTokensReservedEvent>
    {
        private const string TxType = "Purchase Real Estate";
        private const string ReceiverName = "Real Estate";

        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ITransactionReportRepository _reportHelper;

        public PaymentTransferTokensReservedHandler(
            ICustomerProfileClient customerProfileClient,
            ITransactionReportRepository reportHelper
        )
        {
            _customerProfileClient = customerProfileClient;
            _reportHelper = reportHelper;
        }

        public async Task HandleAsync(PaymentTransferTokensReservedEvent msg)
        {
            // TODO: implement deduplication later
            var senderResponse = await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(msg.CustomerId);
            var sender = senderResponse?.Profile;

            await _reportHelper.AddAsync(
                new TransactionReport
                {
                    Id = msg.TransferId,
                    Timestamp =  msg.Timestamp ,
                    TransactionType = TxType,
                    Status = TxStatus.Reserved.ToString(),
                    Vertical = Vertical.RealEstate.ToString(),
                    TransactionCategory = TxCategory.Redemption.ToString(),
                    Amount = msg.Amount,
                    SenderName = $"{sender?.FirstName} {sender?.LastName}",
                    SenderEmail = sender?.Email,
                    ReceiverName = ReceiverName,
                    LocationInfo = msg.LocationCode,
                }
            );
        }
    }
}
