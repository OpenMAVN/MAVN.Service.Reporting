using System.Threading.Tasks;
using Lykke.Service.Reporting.Domain;
using Lykke.Service.Reporting.Domain.Repositories;
using Lykke.Service.Reporting.Domain.Services;
using Lykke.Service.Vouchers.Contract;

namespace Lykke.Service.Reporting.DomainServices.EventHandlers
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
