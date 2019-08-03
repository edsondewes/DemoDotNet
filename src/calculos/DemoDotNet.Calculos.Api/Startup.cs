using System;
using System.IO;
using System.Reflection;
using DemoDotNet.Calculos.Api.Handlers;
using DemoDotNet.Calculos.Api.HealthChecks;
using DemoDotNet.Calculos.Core;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DemoDotNet.Calculos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddHealthChecks()
                .AddCheck<TaxasAPIHealthCheck>("Taxas API");

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMediatR(typeof(Startup), typeof(EnderecoRepositorioConsulta));
            services.AddHttpClient<ITaxasClient, TaxasClientImpl>(client =>
            {
                var baseAddress = Configuration
                    .GetSection("TaxasClient")
                    .GetValue<string>("BaseAddress");

                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Contact = new Contact
                    {
                        Name = "Edson Dewes",
                        Url = "https://github.com/edsondewes"
                    },
                    Description = "Execução de cálculos monetários",
                    Title = "Calculos API",
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
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";

                        await context.Response.WriteAsync("Oops.. ocorreu um erro não esperado");
                    });
                });
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
