using System.Threading.Tasks;

namespace MAVN.Service.Reporting.Domain.Services
{
    public interface IEventHandler<in TEvent>
    {
        Task HandleAsync(TEvent message);
    }
}
