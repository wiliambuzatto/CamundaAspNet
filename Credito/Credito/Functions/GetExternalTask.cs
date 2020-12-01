using System;
using System.Threading.Tasks;
using Credito.Servicos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Credito.Functions
{
    public class GetExternalTask
    {
        private readonly ICamundaService _camundaService;
        private readonly ServiceBusQueueService _serviceBusQueueService;

        public GetExternalTask(ICamundaService camundaService, ServiceBusQueueService serviceBusQueueService)
        {
            _camundaService = camundaService;
            _serviceBusQueueService = serviceBusQueueService;
        }

        [FunctionName("GetExternalTask")]
        public async Task Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            var response = await _camundaService.GetExternalTasks();
            foreach (var task in response)
            {
                await _serviceBusQueueService.SendMessage(task.TopicName, JsonConvert.SerializeObject(task));
            }

            log.LogInformation($"Processando {response.Count} external tasks");
        }
    }
}
