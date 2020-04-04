using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Sdk.Health;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.Reporting.DomainServices.Services;
using MAVN.Service.Reporting.Services;
using MAVN.Service.Reporting.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.Reporting.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly ReportSettings _settings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _settings = appSettings.CurrentValue.ReportService;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // NOTE: Do not register entire settings in container, pass necessary settings to services which requires them

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            builder.RegisterType<TransactionReportReader>()
                .As<ITransactionReportReader>()
                .SingleInstance();

            builder.RegisterType<VerticalResolver>()
                .As<IVerticalResolver>()
                .SingleInstance();

            builder.RegisterType<ReferralInfoFetcher>()
                .As<IReferralInfoFetcher>()
                .SingleInstance();
        }
    }
}
