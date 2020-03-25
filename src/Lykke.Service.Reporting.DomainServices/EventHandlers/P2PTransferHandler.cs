using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.Reporting.Domain;
using Lykke.Service.Reporting.Domain.Models;
using Lykke.Service.Reporting.Domain.Repositories;
using Lykke.Service.Reporting.Domain.Services;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Service.Reporting.DomainServices.EventHandlers
{
    public class P2PTransferHandler : IEventHandler<P2PTransferEvent>
    {
        private const string TxType = "P2PTransfer";

        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ITransactionReportRepository _reportHelper;

        public P2PTransferHandler(
            ICustomerProfileClient customerProfileClient,
            ITransactionReportRepository reportHelper
            )
        {
            _customerProfileClient = customerProfileClient;
            _reportHelper = reportHelper;
        }

        public async Task HandleAsync(P2PTransferEvent msg)
        {
            // TODO: implement deduplication later

            var profiles = await _customerProfileClient.CustomerProfiles.GetByIdsAsync(
                new []{ msg.SenderCustomerId, msg.ReceiverCustomerId },
                true,
                true);

            var sender = profiles.FirstOrDefault(p => p.CustomerId == msg.SenderCustomerId);
            var receiver = profiles.FirstOrDefault(p => p.CustomerId == msg.ReceiverCustomerId);

            await _reportHelper.AddAsync(
                new TransactionReport
                {
                    Id = msg.TransactionId,
                    Timestamp =  msg.Timestamp ,
                    Amount = msg.Amount,
                    TransactionType = TxType,
                    TransactionCategory = TxCategory.P2P.ToString(),
                    SenderName = $"{sender?.FirstName} {sender?.LastName}",
                    SenderEmail = sender?.Email,
                    ReceiverName = $"{receiver?.FirstName} {receiver?.LastName}",
                    ReceiverEmail = receiver?.Email,
                }
            );
        }
    }
}
