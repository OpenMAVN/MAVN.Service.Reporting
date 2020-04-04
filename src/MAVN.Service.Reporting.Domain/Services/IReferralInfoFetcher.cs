using System.Threading.Tasks;
using MAVN.Service.Reporting.Domain.Models;

namespace MAVN.Service.Reporting.Domain.Services
{
    public interface IReferralInfoFetcher
    {
        Task<ReferralInfo> FetchReferralInfoAsync(string referralId);
    }
}
