using System;
using System.Net;
using System.Net.Http;
using DemoDotNet.Calculos.Api;
using DemoDotNet.Calculos.Testes.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xbehave;
using Xunit;

namespace DemoDotNet.Calculos.Testes.Controllers
{
    public class JurosControllerTestes : HostStartupFixture
    {
        public JurosControllerTestes(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Scenario]
        public void GetJurosCompostosDeveRetornarResultadoDoCalculo(HttpResponseMessage response, Exception exception)
        {
            "Dada a requisição GET para a URL /calculajuros?valorInicial=100&meses=5"
                .x(async () => response = await HttpClient.GetAsync($"/calculajuros?valorInicial=100&meses=5"));

            "Então o status da requisição deve ser de sucesso"
                .x(() => Assert.True(response.IsSuccessStatusCode));

            "Dada a tentativa de leitura do valor de retorno"
                .x(async () => exception = await Record.ExceptionAsync(() => response.Content.ReadAsAsync<decimal>()));

            "Então não deve ocorrer exception ao ler o valor decimal"
                .x(() => Assert.Null(exception));
        }

        [Scenario]
        [Example(null, 1, "É obrigatório informar o valor inicial do cálculo")]
        [Example(1, null, "É obrigatório informar o número de meses do cálculo")]
        [Example(1, -1, "Infome um valor maior que zero")]
        public void GetJurosCompostosDeveValidarParametros(int? valorInicial, int? meses, string mensagem)
        {
            HttpResponseMessage response = null;
            dynamic conteudo = null;

            $"Dada a requisição GET para a URL /calculajuros?valorInicial={valorInicial}&meses={meses}"
                .x(async () => response = await HttpClient.GetAsync($"/calculajuros?valorInicial={valorInicial}&meses={meses}"));

            "Então o status da requisição deve ser BadRequest"
                .x(() => Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode));

            "Dado o conteúdo resposta"
                .x(async () =>
                {
                    var json = await response.Content.ReadAsStringAsync();
                    conteudo = JsonConvert.DeserializeObject(json);
                });

            $"Então as mensagens de erro deve ser: {mensagem}"
                .x(() =>
                {
                    var erros = conteudo.errors.Meses ?? conteudo.errors.ValorInicial;
                    string primeiroErro = erros[0];
                    Assert.Equal(mensagem, primeiroErro);
                });
        }
    }
}
