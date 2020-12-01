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
    public class LiberarCredito
    {
        private readonly ICamundaService _camundaService;
        public LiberarCredito(ICamundaService camundaService)
        {
            _camundaService = camundaService;
        }

        [FunctionName("LiberarCredito")]
        public async Task Run([ServiceBusTrigger("libera.credito", Connection = "ServiceBusConnectionString")]ExternalTask task, ILogger log)
        {
            // Fazer o que preciar

            var variables = new Dictionary<string, Variable> { };

            await _camundaService.CompleteExternalTask(
                                    task.Id.ToString(),
                                    new CompleteExternalTaskRequest { WorkerId = "CreditoProcess", Variables = variables }
                                    );

            log.LogInformation($"{task.BusinessKey} Credito liberado");
        }
    }
}
