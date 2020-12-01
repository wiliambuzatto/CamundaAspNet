using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Credito.Servicos
{
    public class ServiceBusQueueService
    {
        private IDictionary<string, IQueueClient> _queuesClient;
        private readonly string _serviceBusConnectionString;

        public ServiceBusQueueService()
        {
            _serviceBusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            _queuesClient = new Dictionary<string, IQueueClient>();
        }

        public async Task SendMessage(string queueName, string mensagem)
        {
            IQueueClient queueClient = null;

            lock (_queuesClient)
            {
                if (!_queuesClient.TryGetValue(queueName, out queueClient))
                {
                    queueClient = new QueueClient(_serviceBusConnectionString,
                                                  queueName);
                    _queuesClient.Add(queueName, queueClient);
                }
            }

            var message = new Message(Encoding.UTF8.GetBytes(mensagem));

            await queueClient.SendAsync(message);
        }
    }
}
