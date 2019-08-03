using MediatR;

namespace DemoDotNet.Calculos.Core
{
    /// <summary>
    /// Requisição de consulta da taxa de juros
    /// </summary>
    public class TaxaJurosConsulta : IRequest<decimal>
    {
    }
}
