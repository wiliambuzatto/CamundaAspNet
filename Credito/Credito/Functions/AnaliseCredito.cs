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
    public class AnaliseCredito
    {
        private readonly ICamundaService _camundaService;

        public AnaliseCredito(ICamundaService camundaService)
        {
            _camundaService = camundaService;
        }

        [FunctionName("AnaliseCredito")]
        public async Task Run([ServiceBusTrigger("analise", Connection = "ServiceBusConnectionString")]ExternalTask task, ILogger log)
        {
            // Executar o que precisar

            var variables = new Dictionary<string, Variable>
            {
                { "creditoAprovado", new Variable{ Type = "boolean", Value = false } }
            };

            await _camundaService.CompleteExternalTask(
                task.Id.ToString(), 
                new CompleteExternalTaskRequest { WorkerId = "CreditoProcess", Variables = variables });

            log.LogInformation($"{task.BusinessKey} Analise de credito concluida");
        }
    }
}
