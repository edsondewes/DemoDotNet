using System.ComponentModel.DataAnnotations;

namespace DemoDotNet.Calculos.Api.ViewModels
{
    /// <summary>
    /// Modelo de parâmetros recebidos pelo endpoint de cálculo de juros compostos
    /// </summary>
    public class JurosCompostosModel
    {
        [Required(ErrorMessage = "É obrigatório informar o valor inicial do cálculo")]
        public decimal? ValorInicial { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o número de meses do cálculo")]
        [Range(
            minimum: 1,
            maximum: int.MaxValue,
            ErrorMessage = "Infome um valor maior que zero"
            )]
        public int? Meses { get; set; }
    }
}
