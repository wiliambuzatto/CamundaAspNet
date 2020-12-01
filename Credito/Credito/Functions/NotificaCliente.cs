using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Credito.Models;
using Credito.Servicos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Credito.Functions
{
    public class NotificaCliente
    {
        private readonly ICamundaService _camundaService;

        public NotificaCliente(ICamundaService camundaService)
        {
            _camundaService = camundaService;
        }

        [FunctionName("NotificaCliente")]
        public async Task Run([ServiceBusTrigger("notifica.cliente", Connection = "ServiceBusConnectionString")]ExternalTask task, ILogger log)
        {
            var variables = new Dictionary<string, Variable> { };

            await _camundaService.CompleteExternalTask(task.Id.ToString(),
                                                      new CompleteExternalTaskRequest { WorkerId = "CreditoProcess", Variables = variables }
                                                      );

            log.LogInformation($"{task.BusinessKey} Cliente notificado");

        }
    }
}
