using System.Threading.Tasks;

namespace Lykke.Service.Reporting.Domain.Services
{
    public interface IVerticalResolver
    {
        Task<Vertical> ResolveVerticalAsync(string conditionType);
    }
}
