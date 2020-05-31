using System.Threading.Tasks;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.WalletManagement.Contract.Events;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
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
