using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DemoDotNet.Calculos.Api;
using DemoDotNet.Calculos.Api.Handlers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xbehave;
using Xunit;

namespace DemoDotNet.Calculos.Testes.Fixtures
{
    public class HostStartupFixture : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public HttpClient HttpClient { get; private set; }

        public HostStartupFixture(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(c => c.ConfigureTestServices(services =>
            {
                services.RemoveAll<ITaxasClient>();

                var taxasClientMock = new Mock<ITaxasClient>();
                taxasClientMock
                    .Setup(obj => obj.GetTaxaJuros(It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(0.01m));

                services.TryAddSingleton(taxasClientMock.Object);
            }));

        }

        [Background]
        public void CriarHttpClient()
        {
            "Dado o client http"
                .x(() => HttpClient = _factory.CreateClient());
        }
    }
}
