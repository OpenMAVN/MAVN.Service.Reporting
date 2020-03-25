using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.Campaign.Client;
using Lykke.Service.Campaign.Client.Models.Enums;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.Reporting.Domain.Models;
using Lykke.Service.Reporting.Domain.Repositories;
using Lykke.Service.Reporting.Domain.Services;
using Lykke.Service.Staking.Contract.Events;

namespace Lykke.Service.Reporting.DomainServices.EventHandlers
{
    public class ReferralStakeBurntHandler : IEventHandler<ReferralStakeBurntEvent>
    {
        private const string TxType = "Stake Burnt";

        private readonly ICampaignClient _campaignClient;
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly IReferralInfoFetcher _referralInfoFetcher;
        private readonly ITransactionReportRepository _reportHelper;
        private readonly IVerticalResolver _verticalResolver;
        private readonly ILog _log;

        public ReferralStakeBurntHandler(
            ICampaignClient campaignClient,
            ICustomerProfileClient customerProfileClient,
            IReferralInfoFetcher referralInfoFetcher,
            ITransactionReportRepository reportHelper,
            IVerticalResolver verticalResolver,
            ILogFactory logFactory
        )
        {
            _campaignClient = campaignClient;
            _customerProfileClient = customerProfileClient;
            _referralInfoFetcher = referralInfoFetcher;
            _reportHelper = reportHelper;
            _verticalResolver = verticalResolver;
            _log = logFactory.CreateLog(this);
        }

        public async Task HandleAsync(ReferralStakeBurntEvent msg)
        {
            // TODO: implement deduplication later

            var campaign = await _campaignClient.History.GetEarnRuleByIdAsync(Guid.Parse(msg.CampaignId));
            if (campaign.ErrorCode != CampaignServiceErrorCodes.EntityNotFound)
            {
                _log.Warning("Campaign not found for bonus event",
                    context: new { customerId = msg.CustomerId, error = campaign.ErrorCode.ToString() });
            }

            var senderResponse = await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(msg.CustomerId);
            var sender = senderResponse?.Profile;

            var condition = campaign.Conditions?.FirstOrDefault(c => c.Id == msg.CampaignId);
            var vertical = await _verticalResolver.ResolveVerticalAsync(condition?.Type);

            string info = null;
            if (!string.IsNullOrWhiteSpace(msg.ReferralId))
            {
                var referralInfo = await _referralInfoFetcher.FetchReferralInfoAsync(msg.ReferralId);
                if (referralInfo != null)
                    info = $"Staking amount burnt when referring {referralInfo.Email}";
            }

            await _reportHelper.AddAsync(
                new TransactionReport
                {
                    Id = Guid.NewGuid().ToString(),
                    Timestamp = msg.Timestamp,
                    Amount = msg.Amount,
                    TransactionType = TxType,
                    Vertical = vertical.ToString(),
                    CampaignName = campaign.Name,
                    Info = info,
                    SenderName = $"{sender?.FirstName} {sender?.LastName}",
                    SenderEmail = sender?.Email,
                }
            );
        }
    }
}
