using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using TheFleet.Services.Constants;
using TheFleet.Services.Context;
using TheFleet.Services.Dependencies;

namespace TheFleet.Services
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
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = "https://localhost:44327/";
                    config.Audience = "ApiOne";
                });

            this.ConfigureLogger();
            services.ResolveDependencies();
            services.ResolveValidatorsDependencies();

            services.AddEntityFrameworkNpgsql().AddDbContext<TheFleetContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString(ApplicationSettingsConstants.TheFleetDb)));
            services.AddControllers();
            services.AddMvc()
                .AddFluentValidation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureLogger()
        {
            var elasticUri = Configuration.GetValue<Uri>(ApplicationSettingsConstants.ElasticSearchUri);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
                {
                    AutoRegisterTemplate = true,
                    CustomFormatter = new ElasticsearchJsonFormatter()
                })
                .CreateLogger();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();
        }
    }
}
