using System;
using System.Threading;
using System.Threading.Tasks;
using DemoDotNet.Calculos.Api.Handlers;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DemoDotNet.Calculos.Api.HealthChecks
{
    public class TaxasAPIHealthCheck : IHealthCheck
    {
        private readonly ITaxasClient _taxasClient;

        public TaxasAPIHealthCheck(ITaxasClient taxasClient)
        {
            _taxasClient = taxasClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var status = await _taxasClient.GetHealthCheck(cancellationToken);
                if (status == HealthStatus.Healthy)
                {
                    return HealthCheckResult.Healthy("Consulta de taxas está operacional");
                }

                return HealthCheckResult.Degraded("Consulta de taxas não está operacional");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Degraded("Consulta de taxas não está operacional", ex);
            }
        }
    }
}
