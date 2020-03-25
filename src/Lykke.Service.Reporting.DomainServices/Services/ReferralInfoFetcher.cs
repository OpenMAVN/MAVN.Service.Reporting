using System;
using System.Threading.Tasks;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.CustomerProfile.Client.Models.Enums;
using Lykke.Service.Reporting.Domain.Models;
using Lykke.Service.Reporting.Domain.Services;

namespace Lykke.Service.Reporting.DomainServices.Services
{
    public class ReferralInfoFetcher : IReferralInfoFetcher
    {
        private readonly ICustomerProfileClient _customerProfileClient;

        public ReferralInfoFetcher(ICustomerProfileClient customerProfileClient)
        {
            _customerProfileClient = customerProfileClient;
        }

        public async Task<ReferralInfo> FetchReferralInfoAsync(string referralId)
        {
            var referralGuid = Guid.Parse(referralId);

            var friendReferral = await _customerProfileClient.ReferralFriendProfiles.GetByIdAsync(referralGuid);
            if (friendReferral.ErrorCode == ReferralFriendProfileErrorCodes.None)
                return new ReferralInfo { Email = friendReferral.Data.Email };

            var hotelReferral = await _customerProfileClient.ReferralHotelProfiles.GetByIdAsync(referralGuid);
            if (hotelReferral.ErrorCode == ReferralHotelProfileErrorCodes.None)
                return new ReferralInfo { Email = hotelReferral.Data.Email };

            var leadReferral = await _customerProfileClient.ReferralLeadProfiles.GetByIdAsync(referralGuid);
            if (leadReferral.ErrorCode == ReferralLeadProfileErrorCodes.None)
                return new ReferralInfo { Email = leadReferral.Data.Email };

            return null;
        }
    }
}
