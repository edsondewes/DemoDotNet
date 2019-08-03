using DemoDotNet.Taxas.Core;
using Xbehave;
using Xunit;

namespace DemoDotNet.Taxas.Testes
{
    public class TaxaJurosTestes
    {
        [Scenario]
        public void ConsultaDeveRetornarValorFixo(TaxaJurosConsulta consulta, TaxaJurosValorFixoHandler handler, decimal resultado)
        {
            "Dada uma consulta de juros sem parâmetros"
                .x(() => consulta = new TaxaJurosConsulta());

            "E um handler de valor fixo"
                .x(() => handler = new TaxaJurosValorFixoHandler());

            "Quando realizarmos a consulta"
                .x(async () => resultado = await handler.Handle(consulta, default));

            "Então a taxa de juros deve ser 0.01"
                .x(() => Assert.Equal(0.01m, resultado));
        }
    }
}
