using System;
using System.Threading.Tasks;
using Lykke.Service.CrossChainTransfers.Contract;
using Lykke.Service.CustomerProfile.Client;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class TransferToExternalProcessedHandler : IEventHandler<TransferToExternalProcessedEvent>
    {
        private const string TxType = "Transfer To External Wallet";

        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ITransactionReportRepository _reportHelper;

        public TransferToExternalProcessedHandler(
            ICustomerProfileClient customerProfileClient,
            ITransactionReportRepository reportHelper
        )
        {
            _customerProfileClient = customerProfileClient;
            _reportHelper = reportHelper;
        }
        
        public async Task HandleAsync(TransferToExternalProcessedEvent msg)
        {
            // TODO: implement deduplication later

            var senderResponse = await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(msg.CustomerId);
            var sender = senderResponse?.Profile;

            await _reportHelper.AddAsync(
                new TransactionReport
                {
                    Id = msg.OperationId,
                    Timestamp = DateTime.UtcNow,
                    Amount = msg.Amount,
                    TransactionType = TxType,
                    Vertical = Vertical.LoyaltySystem.ToString(),
                    Status = TxStatus.Completed.ToString(),
                    SenderName = $"{sender?.FirstName} {sender?.LastName}",
                    SenderEmail = sender?.Email,
                }
            );
        }
    }
}
