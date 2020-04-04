using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.Reporting.Domain.Services;

namespace MAVN.Service.Reporting.DomainServices.RabbitSubscribers
{
    public class RabbitSubscriber<TEvent> : JsonRabbitSubscriber<TEvent>
    {
        private readonly IEventHandler<TEvent> _handler;
        private readonly ILog _log;

        public RabbitSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            IEventHandler<TEvent> handler,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _handler = handler;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(TEvent message)
        {
            _log.Info($"Received {typeof(TEvent).Name}", message);

            await _handler.HandleAsync(message);

            _log.Info($"Handled {typeof(TEvent).Name}", message);
        }
    }
}
