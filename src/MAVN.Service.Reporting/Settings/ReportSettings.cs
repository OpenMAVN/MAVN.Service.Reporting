using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.Reporting.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ReportSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings Rabbit { get; set; }

        [Optional]
        public bool? IsPublicBlockchainFeatureDisabled { get; set; }
    }
}
