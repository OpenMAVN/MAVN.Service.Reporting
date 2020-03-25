using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Reporting.Client 
{
    /// <summary>
    /// Report client settings.
    /// </summary>
    public class ReportServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
