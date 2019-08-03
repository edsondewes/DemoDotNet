using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DemoDotNet.Calculos.Core.Handlers
{
    /// <summary>
    /// Executor da requisição de cálculo de juros compostos
    /// </summary>
    public class JurosCompostosHandler : IRequestHandler<JurosCompostosCalculo, decimal>
    {
        private readonly IMediator _mediator;

        public JurosCompostosHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<decimal> Handle(JurosCompostosCalculo request, CancellationToken cancellationToken)
        {
            // O cálculo deve seguir a seguinte definição:
            // Valor Final = Valor Inicial * (1 + Juros) ^ Meses

            // A taxa de juros é definida por uma consulta externa
            var taxaJuros = (double)await _mediator.Send(new TaxaJurosConsulta(), cancellationToken);
            var potencia = (decimal)Math.Pow(1 + taxaJuros, request.Meses);

            var juros = request.ValorInicial * potencia;
            var valorTruncado = Math.Truncate(juros * 100) / 100;

            return valorTruncado;
        }
    }
}
