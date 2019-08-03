using System;
using MediatR;

namespace DemoDotNet.Calculos.Core
{
    /// <summary>
    /// Requisição do cálculo de juros compostos
    /// </summary>
    public class JurosCompostosCalculo : IRequest<decimal>
    {
        public decimal ValorInicial { get; }
        public int Meses { get; }

        public JurosCompostosCalculo(decimal valorInicial, int meses)
        {
            if (meses <= 0)
            {
                throw new ArgumentException("O número de meses deve ser maior que zero", nameof(meses));
            }

            ValorInicial = valorInicial;
            Meses = meses;
        }
    }
}
