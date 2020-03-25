using System.Threading.Tasks;

namespace Lykke.Service.Reporting.Domain.Services
{
    public interface IEventHandler<in TEvent>
    {
        Task HandleAsync(TEvent message);
    }
}
