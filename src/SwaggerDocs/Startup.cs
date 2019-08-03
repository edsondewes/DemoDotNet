using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace SwaggerDocs
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                var config = Configuration.GetSection("Swagger").Get<SwaggerConfig>();
                if (config is null || config.Endpoints is null || config.Endpoints.Length == 0)
                {
                    throw new Exception("Nenhum endpoint swagger foi definido");
                }

                foreach (var endpoint in config.Endpoints)
                {
                    c.SwaggerEndpoint(endpoint.Url, endpoint.Nome);
                }

                // Endpoint do swagger vai responder em /
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
