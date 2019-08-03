using System.Net.Http;
using DemoDotNet.Calculos.Api;
using DemoDotNet.Calculos.Testes.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Xbehave;
using Xunit;

namespace DemoDotNet.Calculos.Testes.Controllers
{
    public class ShowMeTheCodeControllerTestes : HostStartupFixture
    {
        public ShowMeTheCodeControllerTestes(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Scenario]
        public void GetShowMeTheCodeDeveRetornarStatusSucesso(HttpResponseMessage response, string url)
        {
            "Dada a requisição GET para a URL /showmethecode"
                .x(async () => response = await HttpClient.GetAsync("/showmethecode"));

            "Então o status da requisição deve ser de sucesso"
                .x(() => Assert.True(response.IsSuccessStatusCode));

            "Dado o conteúdo do retorno"
                .x(async () => url = await response.Content.ReadAsStringAsync());

            "Então deve ser um endereço http"
                .x(() => Assert.StartsWith("http", url));
        }
    }
}
