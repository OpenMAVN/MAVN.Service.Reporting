using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Reporting.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        [SqlCheck]
        public string SqlDbConnString { get; set; }
    }
}
