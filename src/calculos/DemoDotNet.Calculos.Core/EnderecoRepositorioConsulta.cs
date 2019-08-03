using MediatR;

namespace DemoDotNet.Calculos.Core
{
    /// <summary>
    /// Requisição de consulta de endereço do repositório
    /// </summary>
    public class EnderecoRepositorioConsulta : IRequest<string>
    {
    }
}
