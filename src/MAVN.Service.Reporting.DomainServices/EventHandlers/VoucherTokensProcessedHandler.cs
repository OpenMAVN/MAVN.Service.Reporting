using System.Threading.Tasks;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using Lykke.Service.Vouchers.Contract;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class VoucherTokensProcessedHandler : IEventHandler<VoucherTokensUsedEvent>
    {
        private readonly ITransactionReportRepository _reportHelper;

        public VoucherTokensProcessedHandler(ITransactionReportRepository reportHelper)
        {
            _reportHelper = reportHelper;
        }

        public async Task HandleAsync(VoucherTokensUsedEvent msg)
        {
            await _reportHelper.UpdateStatusAsync(msg.TransferId.ToString(), TxStatus.Completed);
        }
    }
}
