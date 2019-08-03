using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DemoDotNet.Calculos.Core.Handlers
{
    /// <summary>
    /// Executor da requisição de endereço do repositório
    /// </summary>
    public class EnderecoRepositorioValorFixoHandler : IRequestHandler<EnderecoRepositorioConsulta, string>
    {
        // Como o resultado é fixo, podemos cachear a task de retorno
        private readonly Task<string> _enderecoRepositorioFixo = Task.FromResult("https://github.com/edsondewes/DemoDotNet");

        public Task<string> Handle(EnderecoRepositorioConsulta request, CancellationToken cancellationToken)
        {
            return _enderecoRepositorioFixo;
        }
    }
}
