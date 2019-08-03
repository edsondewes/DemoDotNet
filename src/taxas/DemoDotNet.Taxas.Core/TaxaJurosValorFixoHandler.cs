using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DemoDotNet.Taxas.Core
{
    /// <summary>
    /// Executor da requisição de taxas de juros com valor fixo
    /// </summary>
    public class TaxaJurosValorFixoHandler : IRequestHandler<TaxaJurosConsulta, decimal>
    {
        // Como o resultado é fixo, podemos cachear a task de retorno
        private readonly Task<decimal> _taxaJurosValorFixo = Task.FromResult(0.01m);

        public Task<decimal> Handle(TaxaJurosConsulta request, CancellationToken cancellationToken)
        {
            return _taxaJurosValorFixo;
        }
    }
}
