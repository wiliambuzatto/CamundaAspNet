using Credito.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Credito.Servicos
{
    public class CamundaService : ICamundaService
    {
        private readonly UserAgent _userAgent;
        private readonly string _url;
        public CamundaService(UserAgent userAgent)
        {
            _userAgent = userAgent;
            _url = Environment.GetEnvironmentVariable("CamundaUrl");
        }

        public async Task StartProcess(string ProcessKey, Process process)
        {
            await _userAgent.PostAsJsonAsync($"{_url}/process-definition/key/{ProcessKey}/start", process);
        }

        public async Task<List<ExternalTask>> GetExternalTasks()
        {

            var request = new
            {
                workerId = "CreditoProcess",
                maxTasks = 10,
                topics = new[]
                    {
                        new {
                            topicName = "analise",
                            lockDuration = 6000
                        },
                        new {
                            topicName = "libera.credito",
                            lockDuration = 6000
                        },
                         new {
                            topicName = "notifica.cliente",
                            lockDuration = 6000
                        },
                    }
            };

            var requestBoddy = JsonConvert.SerializeObject(request);

            return await _userAgent.PostAsJsonAsync<List<ExternalTask>>($"{_url}/external-task/fetchAndLock", requestBoddy);

        }

        public async Task CompleteExternalTask(string id, CompleteExternalTaskRequest request)
        {
            await _userAgent.PostAsJsonAsync<List<Dictionary<string, Variable>>>($"{_url}/external-task/{id}/complete", JsonConvert.SerializeObject(request));
        }
    }
}
