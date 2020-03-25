using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Lykke.Service.Campaign.Client;
using Lykke.Service.Campaign.Client.Models.BonusType;
using Lykke.Service.Reporting.Domain;
using Lykke.Service.Reporting.Domain.Services;

namespace Lykke.Service.Reporting.DomainServices.Services
{
    public class VerticalResolver : IVerticalResolver
    {
        private readonly ICampaignClient _campaignClient;
        private readonly ConcurrentDictionary<string, BonusTypeModel> _bonusTypeDict = new ConcurrentDictionary<string, BonusTypeModel>();

        public VerticalResolver(
            ICampaignClient campaignClient)
        {
            _campaignClient = campaignClient;
        }

        public async Task<Vertical> ResolveVerticalAsync(string conditionType)
        {
            if (string.IsNullOrWhiteSpace(conditionType))
                return Vertical.LoyaltySystem;

            if (!_bonusTypeDict.TryGetValue(conditionType, out var bonusType))
            {
                bonusType = await _campaignClient.BonusTypes.GetByTypeAsync(conditionType);
                _bonusTypeDict.TryAdd(conditionType, bonusType);
            }

            if (!bonusType.Vertical.HasValue)
                return Vertical.Unknown;

            switch (bonusType.Vertical.Value)
            {
                case PartnerManagement.Client.Models.Vertical.Hospitality:
                    return Vertical.Hospitality;
                case PartnerManagement.Client.Models.Vertical.RealEstate:
                    return Vertical.RealEstate;
                case PartnerManagement.Client.Models.Vertical.Retail:
                    return Vertical.Retail;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
