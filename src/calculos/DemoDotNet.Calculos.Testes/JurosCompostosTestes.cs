using System;
using System.Threading;
using System.Threading.Tasks;
using DemoDotNet.Calculos.Core;
using DemoDotNet.Calculos.Core.Handlers;
using MediatR;
using Moq;
using Xbehave;
using Xunit;

namespace DemoDotNet.Calculos.Testes
{
    public class JurosCompostosTestes
    {
        [Scenario]
        public void CalculoNaoDevePermitirValorNegativoParaParametroMeses(Exception exception)
        {
            "Dado um cálculo com parâmetro 'Meses' com valor negativo"
                .x(() => exception = Record.Exception(() => new JurosCompostosCalculo(10, -1)));

            "Então deve lançar exception na inicialização do cálculo"
                .x(() => Assert.NotNull(exception));
        }

        [Scenario]
        [Example(100, 5, 105.10d)]
        [Example(100, 6, 106.15d)]
        [Example(100, 9, 109.36d)]
        public void CalculoDeveRetornarValorComDuasCasasDecimais(int valorInicial, int meses, double resultadoEsperado)
        {
            JurosCompostosCalculo calculo = null;
            JurosCompostosHandler handler = null;
            decimal resultado = default;

            $"Dado um valor inicial {valorInicial} e um número de meses {meses}"
                .x(() => calculo = new JurosCompostosCalculo(valorInicial, meses));

            "E um handler com taxa de juros 0.01"
                .x(() =>
                {
                    var mediator = new Mock<IMediator>();
                    mediator
                        .Setup(obj => obj.Send(It.IsAny<TaxaJurosConsulta>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(0.01m));

                    handler = new JurosCompostosHandler(mediator.Object);
                });

            "Quando realizarmos o cálculo"
                .x(async () => resultado = await handler.Handle(calculo, default));

            "Então o valor do resultado deve ser igual a 105.1"
                .x(() => Assert.Equal((decimal)resultadoEsperado, resultado));
        }
    }
}
