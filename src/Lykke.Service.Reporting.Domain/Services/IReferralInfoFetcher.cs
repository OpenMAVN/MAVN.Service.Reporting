using System.Threading.Tasks;
using Lykke.Service.Reporting.Domain.Models;

namespace Lykke.Service.Reporting.Domain.Services
{
    public interface IReferralInfoFetcher
    {
        Task<ReferralInfo> FetchReferralInfoAsync(string referralId);
    }
}
