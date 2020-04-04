using System.Threading.Tasks;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using Lykke.Service.WalletManagement.Contract.Events;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{ 
    public class RefundPaymentTransferHandler : IEventHandler<RefundPaymentTransferEvent>
    {
        private readonly ITransactionReportRepository _reportHelper;

        public RefundPaymentTransferHandler(ITransactionReportRepository reportHelper)
        {
            _reportHelper = reportHelper;
        }

        public async Task HandleAsync(RefundPaymentTransferEvent msg)
        {
            // TODO: implement deduplication later

            await _reportHelper.UpdateStatusAsync(msg.TransferId, TxStatus.Rejected);
        }
    }
}
