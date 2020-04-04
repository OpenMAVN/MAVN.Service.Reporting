using System.Threading.Tasks;
using Lykke.Service.PaymentTransfers.Contract;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class PaymentTransferProcessedHandler : IEventHandler<PaymentTransferProcessedEvent>
    {
        private readonly ITransactionReportRepository _reportHelper;

        public PaymentTransferProcessedHandler(ITransactionReportRepository reportHelper)
        {
            _reportHelper = reportHelper;
        }

        public async Task HandleAsync(PaymentTransferProcessedEvent msg)
        {
            if (msg.Status != ProcessedPaymentTransferStatus.Accepted)
                return;

            // TODO: implement deduplication later

            await _reportHelper.UpdateStatusAsync(msg.TransferId, TxStatus.Completed);
        }
    }
}
