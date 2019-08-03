using System.Threading.Tasks;
using DemoDotNet.Calculos.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoDotNet.Calculos.Api.Controllers
{
    [ApiController]
    public class ShowMeTheCodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShowMeTheCodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Consulta do endereço de publicação do código-fonte
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /showmthecode
        ///
        /// </remarks>
        /// <response code="200">Retorna a URL do GitHub</response>
        /// <returns>URL do reoisitório</returns>
        [HttpGet("/showmethecode")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<string>> Get()
        {
            var endereco = await _mediator.Send(new EnderecoRepositorioConsulta());
            return endereco;
        }
    }
}
