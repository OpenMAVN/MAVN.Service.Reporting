using System.Threading.Tasks;
using Lykke.Service.PaymentTransfers.Contract;
using Lykke.Service.Reporting.Domain;
using Lykke.Service.Reporting.Domain.Repositories;
using Lykke.Service.Reporting.Domain.Services;

namespace Lykke.Service.Reporting.DomainServices.EventHandlers
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
