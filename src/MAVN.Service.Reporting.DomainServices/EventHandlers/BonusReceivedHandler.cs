using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.Campaign.Client;
using MAVN.Service.Campaign.Client.Models.Condition;
using MAVN.Service.Campaign.Client.Models.Enums;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.CustomerProfile.Client.Models.Enums;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.WalletManagement.Contract.Events;

namespace MAVN.Service.Reporting.DomainServices.EventHandlers
{
    public class BonusReceivedHandler : IEventHandler<BonusReceivedEvent>
    {
        private const string TxType = "Bonus Paid";
        private const string CampaignLevelBonusType = "CampaignReward";

        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ITransactionReportRepository _reportHelper;
        private readonly ICampaignClient _campaignClient;
        private readonly IReferralInfoFetcher _referralInfoFetcher;
        private readonly IVerticalResolver _verticalResolver;
        private readonly ILog _log;

        public BonusReceivedHandler(
            ICustomerProfileClient customerProfileClient,
            ITransactionReportRepository reportHelper,
            ICampaignClient campaignClient,
            IReferralInfoFetcher referralInfoFetcher,
            IVerticalResolver verticalResolver,
            ILogFactory logFactory)
        {
            _customerProfileClient = customerProfileClient;
            _reportHelper = reportHelper;
            _campaignClient = campaignClient;
            _referralInfoFetcher = referralInfoFetcher;
            _verticalResolver = verticalResolver;
            _log = logFactory.CreateLog(this);
        }

        public async Task HandleAsync(BonusReceivedEvent msg)
        {
            // TODO: implement deduplication later

            var profileResponse = await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(
                msg.CustomerId,
                true,
                true);

            if (profileResponse.ErrorCode != CustomerProfileErrorCodes.None)
            {
                _log.Warning("Couldn't fetch customer profile from CP",
                    context: new {customerId = msg.CustomerId, error = profileResponse.ErrorCode.ToString()});

                return;
            }

            var profile = profileResponse.Profile;

            ConditionModel condition = null;
            var campaign = await _campaignClient.History.GetEarnRuleByIdAsync(msg.CampaignId);
            if (campaign.ErrorCode == CampaignServiceErrorCodes.EntityNotFound)
            {
                _log.Warning("Campaign not found for bonus event",
                    context: new {customerId = msg.CustomerId, error = campaign.ErrorCode.ToString()});
            }
            else
            {
                condition = campaign.Conditions.FirstOrDefault(c => c.Id == msg.ConditionId.ToString());

                if (condition == null && msg.BonusType != CampaignLevelBonusType)
                {
                    _log.Warning("Campaign does not contain the condition",
                        context: new {campaignId = msg.CampaignId, conditionId = msg.ConditionId});
                }
            }

            Vertical vertical;
            if (!string.IsNullOrWhiteSpace(msg.LocationCode))
                vertical = Vertical.RealEstate;
            else if (!string.IsNullOrWhiteSpace(msg.PartnerId))
                vertical = Vertical.Hospitality;
            else
                vertical = await _verticalResolver.ResolveVerticalAsync(condition?.Type);

            string info = null;
            if (!string.IsNullOrWhiteSpace(msg.ReferralId))
            {
                var referralInfo = await _referralInfoFetcher.FetchReferralInfoAsync(msg.ReferralId);
                if (referralInfo != null)
                    info = $"Referral bonus for {referralInfo.Email}";
            }

            if (!string.IsNullOrWhiteSpace(condition?.TypeDisplayName))
                info = info == null
                    ? condition?.TypeDisplayName
                    : $"{info} ({condition.TypeDisplayName})";

            await _reportHelper.AddAsync(new TransactionReport
                {
                    Id = msg.TransactionId,
                    Timestamp = msg.Timestamp,
                    Amount = msg.Amount,
                    TransactionType = TxType,
                    Vertical = vertical.ToString(),
                    TransactionCategory = TxCategory.Earning.ToString(),
                    CampaignName = campaign.Name,
                    Info = info,
                    ReceiverName = $"{profile.FirstName} {profile.LastName}",
                    ReceiverEmail = profile.Email,
                    CampaignId = msg.CampaignId,
                }
            );
        }
    }
}
