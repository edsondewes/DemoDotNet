using System.Threading.Tasks;
using DemoDotNet.Taxas.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoDotNet.Taxas.Api.Controllers
{
    [ApiController]
    public class TaxasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Consulta da taxa de juros atual
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /taxajuros
        ///
        /// </remarks>
        /// <response code="200">Retorna o valor decimal da taxa</response>
        /// <returns>Valor decimal da taxa</returns>
        [HttpGet("/taxajuros")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<decimal>> GetTaxaJuros()
        {
            var taxaJuros = await _mediator.Send(new TaxaJurosConsulta());
            return taxaJuros;
        }
    }
}
