using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DemoDotNet.Calculos.Api.Handlers
{
    /// <summary>
    /// Cliente de acesso à API de Taxas
    /// </summary>
    public interface ITaxasClient
    {
        /// <summary>
        /// Valida se serviços estão operacionais
        /// </summary>
        /// <returns>Status dos serviços</returns>
        Task<HealthStatus> GetHealthCheck(CancellationToken cancellationToken);

        /// <summary>
        /// Obtém a taxa de juros atual
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Valor da taxa retornada pela API</returns>
        Task<decimal> GetTaxaJuros(CancellationToken cancellationToken);
    }

    /// <summary>
    /// Implementação do cliente da API de Taxas
    /// </summary>
    public class TaxasClientImpl : ITaxasClient
    {
        private readonly HttpClient _client;

        public TaxasClientImpl(HttpClient client)
        {
            _client = client;
        }

        public async Task<HealthStatus> GetHealthCheck(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync("/hc", cancellationToken);
            var content = await response.Content.ReadAsStringAsync();

            return Enum.Parse<HealthStatus>(content);
        }

        public async Task<decimal> GetTaxaJuros(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync("/taxajuros", cancellationToken);
            response.EnsureSuccessStatusCode();

            var taxa = await response.Content.ReadAsAsync<decimal>(cancellationToken);
            return taxa;
        }
    }
}
