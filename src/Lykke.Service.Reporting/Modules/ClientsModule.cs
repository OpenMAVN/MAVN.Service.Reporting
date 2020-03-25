using Autofac;
using JetBrains.Annotations;
using Lykke.Service.Campaign.Client;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.PartnerManagement.Client;
using Lykke.Service.Reporting.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.Reporting.Modules
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
        }
    }
}
