using Autofac;
using JetBrains.Annotations;
using MAVN.Service.Campaign.Client;
using MAVN.Service.CustomerProfile.Client;
using Lykke.Service.PartnerManagement.Client;
using MAVN.Service.Reporting.Settings;
using Lykke.SettingsReader;
using MAVN.Service.SmartVouchers.Client;

namespace MAVN.Service.Reporting.Modules
{
    [UsedImplicitly]
    public class ClientsModule : Module
    {
        private readonly AppSettings _appSettings;

        public ClientsModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings.CurrentValue;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterCustomerProfileClient(_appSettings.CustomerProfileService);
            builder.RegisterPartnerManagementClient(_appSettings.PartnerManagementService);
            builder.RegisterCampaignClient(_appSettings.CampaignService);
            builder.RegisterSmartVouchersClient(_appSettings.SmartVouchersService, null);
        }
    }
}
