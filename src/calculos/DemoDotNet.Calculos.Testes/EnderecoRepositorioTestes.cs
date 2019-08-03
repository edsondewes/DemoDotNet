using DemoDotNet.Calculos.Core;
using DemoDotNet.Calculos.Core.Handlers;
using Xbehave;
using Xunit;

namespace DemoDotNet.Calculos.Testes
{
    public class EnderecoRepositorioTestes
    {
        [Scenario]
        public void ConsultaDeveRetornarValorFixo(EnderecoRepositorioConsulta consulta, EnderecoRepositorioValorFixoHandler handler, string resultado)
        {
            "Dada uma consulta de endereço sem parâmetros"
                .x(() => consulta = new EnderecoRepositorioConsulta());

            "E um handler de valor fixo"
                .x(() => handler = new EnderecoRepositorioValorFixoHandler());

            "Quando realizarmos a consulta"
                .x(async () => resultado = await handler.Handle(consulta, default));

            "Então endereço do repositório deve ser https://github.com/edsondewes/DemoDotNet"
                .x(() => Assert.Equal("https://github.com/edsondewes/DemoDotNet", resultado));
        }
    }
}
