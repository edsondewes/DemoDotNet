using System;
using System.Net.Http;
using DemoDotNet.Taxas.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Xbehave;
using Xunit;

namespace DemoDotNet.Taxas.Testes.Controllers
{
    public class JurosControllerTestes : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient _httpClient;

        public JurosControllerTestes(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Background]
        public void CriarHttpClient()
        {
            "Dado o client http"
                .x(() => _httpClient = _factory.CreateClient());
        }

        [Scenario]
        public void GetTaxasJurosDeveRetornarStatusSucesso(HttpResponseMessage response, Exception exception)
        {
            "Dada a requisição GET para a URL /taxajuros"
                .x(async () => response = await _httpClient.GetAsync("/taxajuros"));

            "Então o status da requisição deve ser de sucesso"
                .x(() => Assert.True(response.IsSuccessStatusCode));

            "Dada a tentativa de leitura do valor de retorno"
                .x(async () => exception = await Record.ExceptionAsync(() => response.Content.ReadAsAsync<decimal>()));

            "Então não deve ocorrer exception ao ler o valor decimal"
                .x(() => Assert.Null(exception));
        }
    }
}
