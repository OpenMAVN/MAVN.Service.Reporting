using JetBrains.Annotations;
using Lykke.Service.Reporting.Client.Api;

namespace Lykke.Service.Reporting.Client
{
    /// <summary>
    /// Report client interface.
    /// </summary>
    [PublicAPI]
    public interface IReportClient
    {
        // Make your app's controller interfaces visible by adding corresponding properties here.
        // NO actual methods should be placed here (these go to controller interfaces, for example - IReportApi).
        // ONLY properties for accessing controller interfaces are allowed.

        /// <summary>Application Api interface</summary>
        IReportApi Api { get; }
    }
}
