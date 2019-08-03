using System.Threading;
using System.Threading.Tasks;
using DemoDotNet.Calculos.Core;
using MediatR;

namespace DemoDotNet.Calculos.Api.Handlers
{
    /// <summary>
    /// Executor da requisição de taxas de juros
    /// </summary>
    public class TaxaJurosHandler : IRequestHandler<TaxaJurosConsulta, decimal>
    {
        private readonly ITaxasClient _client;

        public TaxaJurosHandler(ITaxasClient client)
        {
            _client = client;
        }

        public Task<decimal> Handle(TaxaJurosConsulta request, CancellationToken cancellationToken)
        {
            return _client.GetTaxaJuros(cancellationToken);
        }
    }
}
