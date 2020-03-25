using System.Threading.Tasks;
using Lykke.Service.PartnersPayments.Contract;
using Lykke.Service.Reporting.Domain;
using Lykke.Service.Reporting.Domain.Repositories;
using Lykke.Service.Reporting.Domain.Services;

namespace Lykke.Service.Reporting.DomainServices.EventHandlers
{
    public class PartnersPaymentProcessedHandler : IEventHandler<PartnersPaymentProcessedEvent>
    {
        private readonly ITransactionReportRepository _reportHelper;

        public PartnersPaymentProcessedHandler(ITransactionReportRepository reportHelper)
        {
            _reportHelper = reportHelper;
        }

        public async Task HandleAsync(PartnersPaymentProcessedEvent msg)
        {
            if (msg.Status != ProcessedPartnerPaymentStatus.Accepted)
                return;

            // TODO: implement deduplication later

            await _reportHelper.UpdateStatusAsync(msg.PaymentRequestId, TxStatus.Completed);
        }
    }
}
