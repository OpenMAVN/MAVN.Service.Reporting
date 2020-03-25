using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common;
using Lykke.Common.Log;
using Lykke.Sdk;

namespace Lykke.Service.Reporting.Services
{
    public class ShutdownManager : IShutdownManager
    {
        private readonly IEnumerable<IStartStop> _stoppables;
        private readonly IEnumerable<IStopable> _items;
        private readonly ILog _log;

        public ShutdownManager(
            IEnumerable<IStartStop> stoppables,
            IEnumerable<IStopable> items,
            ILogFactory logFactory)
        {
            _stoppables = stoppables;
            _items = items;
            _log = logFactory.CreateLog(this);
        }

        public async Task StopAsync()
        {
            try
            {
                await Task.WhenAll(_stoppables.Select(i => Task.Run(() => i.Stop())));

                await Task.WhenAll(_items.Select(i => Task.Run(() => i.Stop())));
            }
            catch (Exception ex)
            {
                _log.Warning($"Unable to stop a component", ex);
            }
        }
    }
}
