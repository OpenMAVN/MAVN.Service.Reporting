using Lykke.HttpClientGenerator;
using MAVN.Service.Reporting.Client.Api;

namespace MAVN.Service.Reporting.Client
{
    /// <summary>
    /// Report API aggregating interface.
    /// </summary>
    public class ReportClient : IReportClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to Report Api.</summary>
        public IReportApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public ReportClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IReportApi>();
        }
    }
}
