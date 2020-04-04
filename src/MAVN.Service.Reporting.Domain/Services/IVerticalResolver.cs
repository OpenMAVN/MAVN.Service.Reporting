using System.Threading.Tasks;

namespace MAVN.Service.Reporting.Domain.Services
{
    public interface IVerticalResolver
    {
        Task<Vertical> ResolveVerticalAsync(string conditionType);
    }
}
