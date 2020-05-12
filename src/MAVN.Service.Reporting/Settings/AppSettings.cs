using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using MAVN.Service.Campaign.Client;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.PartnerManagement.Client;
using MAVN.Service.SmartVouchers.Client;

namespace MAVN.Service.Reporting.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public ReportSettings ReportService { get; set; }

        public CustomerProfileServiceClientSettings CustomerProfileService { get; set; }

        public PartnerManagementServiceClientSettings PartnerManagementService { get; set; }

        public CampaignServiceClientSettings CampaignService { get; set; }

        public SmartVouchersServiceClientSettings SmartVouchersService { get; set; }
    }
}
