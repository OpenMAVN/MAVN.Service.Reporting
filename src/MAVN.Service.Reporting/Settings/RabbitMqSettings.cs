using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.Reporting.Settings
{
    public class RabbitMqSettings
    {
        public RabbitMqExchangeSettings Subscribers { get; set; }
    }

    public class RabbitMqExchangeSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }
    }
}
