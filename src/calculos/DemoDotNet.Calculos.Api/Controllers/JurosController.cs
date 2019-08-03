using System.Threading;
using System.Threading.Tasks;
using DemoDotNet.Calculos.Api.ViewModels;
using DemoDotNet.Calculos.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoDotNet.Calculos.Api.Controllers
{
    [ApiController]
    public class JurosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JurosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cálculo de juros compostos de acordo com os parâmetros definidos
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /calculajuros
        ///     {
        ///        "valorInicial": 100,
        ///        "meses": 5
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna o valor dos juros calculados</response>
        /// <response code="400">Se algum dos parâmetros não for definido</response>
        /// <returns>Valor dos juros calculados</returns>
        [HttpGet("/calculajuros")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<decimal>> GetJurosCompostos([FromQuery]JurosCompostosModel dados, CancellationToken cancellationToken)
        {
            var consulta = new JurosCompostosCalculo(
                valorInicial: dados.ValorInicial.Value,
                meses: dados.Meses.Value
                );

            var juros = await _mediator.Send(consulta, cancellationToken);
            return juros;
        }
    }
}
