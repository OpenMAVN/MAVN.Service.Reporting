using System.Threading.Tasks;
using Lykke.Service.Reporting.Domain;
using Lykke.Service.Reporting.Domain.Repositories;
using Lykke.Service.Reporting.Domain.Services;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Service.Reporting.DomainServices.EventHandlers
{
    public class RefundPartnersPaymentHandler : IEventHandler<RefundPartnersPaymentEvent>
    {
        private readonly ITransactionReportRepository _reportHelper;

        public RefundPartnersPaymentHandler(ITransactionReportRepository reportHelper)
        {
            _reportHelper = reportHelper;
        }

        public async Task HandleAsync(RefundPartnersPaymentEvent msg)
        {
            // TODO: implement deduplication later

            await _reportHelper.UpdateStatusAsync(msg.PaymentRequestId, TxStatus.Rejected);
        }
    }
}
