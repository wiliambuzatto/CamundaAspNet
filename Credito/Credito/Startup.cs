using Credito.Servicos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Credito.Startup))]
namespace Credito
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<ICamundaService, CamundaService>();
            builder.Services.AddScoped<ServiceBusQueueService, ServiceBusQueueService>();
            builder.Services.AddScoped<UserAgent, UserAgent>();
        }
    }
}
