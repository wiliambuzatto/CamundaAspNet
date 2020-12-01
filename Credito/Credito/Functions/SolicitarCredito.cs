using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Credito.Servicos;
using Credito.Models;

namespace Credito.Functions
{
    public class SolicitarCredito
    {
        private readonly ICamundaService _camundaService;

        public SolicitarCredito(ICamundaService camundaService)
        {
            _camundaService = camundaService;
        }

        [FunctionName("SolicitarCredito")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string nome = req.Form["Nome"];
            string email = req.Form["Email"];
            string valor = req.Form["Valor"];

            var meuProcesso = new Process
            {
                BusinessKey = email,
                Variables = null
            };


            await _camundaService.StartProcess("ModeloDeCredito", meuProcesso);

            log.LogInformation("Solicitação de credito recebida");

            return new OkResult();

        }
    }
}
