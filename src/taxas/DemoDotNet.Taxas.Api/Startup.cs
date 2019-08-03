using System;
using System.IO;
using System.Reflection;
using DemoDotNet.Taxas.Core;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Swagger;

namespace DemoDotNet.Taxas.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddHealthChecks()
                .AddCheck("SempreOk", () => HealthCheckResult.Healthy("Todos serviços operacionais"));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMediatR(typeof(TaxaJurosConsulta));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Contact = new Contact
                    {
                        Name = "Edson Dewes",
                        Url = "https://github.com/edsondewes"
                    },
                    Description = "Consulta de taxas para cálculos",
                    Title = "Taxas API",
                    Version = "v1"
                });

                // Habilita busca de dados das APIs pelos comentários do código
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(policy => policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                );
            }

            app.UseHealthChecks("/hc");
            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((doc, request) =>
                {
                    // O sistema pode estar atrás de um proxy reverso
                    // No caso do Traefik, o prefixo da url vem pelo header X-Forwarded-Prefix
                    // Se não setarmos o BasePath, a requisição feita pela UI será sem o prefixo
                    if (request.Headers.TryGetValue("X-Forwarded-Prefix", out var prefix))
                    {
                        doc.BasePath = prefix.ToString();
                    }
                });
            });
            app.UseMvc();
        }
    }
}
