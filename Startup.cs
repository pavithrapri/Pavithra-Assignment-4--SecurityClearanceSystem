using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using VisitorSecurityClearance.Services;
using VisitorSecurityClearance.Models;
using VisitorSecurityClearance.Helpers;

namespace VisitorSecurityClearance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Visitor Security Clearance API", Version = "v1" });
            });

            // Register application services
            services.AddSingleton<IVisitorService, VisitorService>();
            services.AddSingleton<ISecurityService, SecurityService>();
            services.AddSingleton<IPassService, PassService>();
            services.AddSingleton<IOfficeService, OfficeService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IManagerService, ManagerService>();

            // Register Cosmos DB services for different models
            services.AddSingleton<ICosmosDbService<Visitor>>(InitializeCosmosClientInstanceAsync<Visitor>(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<ICosmosDbService<Security>>(InitializeCosmosClientInstanceAsync<Security>(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<ICosmosDbService<Manager>>(InitializeCosmosClientInstanceAsync<Manager>(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<ICosmosDbService<Office>>(InitializeCosmosClientInstanceAsync<Office>(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<ICosmosDbService<Pass>>(InitializeCosmosClientInstanceAsync<Pass>(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

            // Add configuration for SendGrid
            services.Configure<SendGridOptions>(Configuration.GetSection("SendGrid"));
        }

        private static async Task<CosmosDbService<T>> InitializeCosmosClientInstanceAsync<T>(IConfigurationSection configurationSection) where T : class
        {
            string databaseName = configurationSection["DatabaseName"];
            string containerName = configurationSection["ContainerName"];
            string account = configurationSection["Account"];
            string key = configurationSection["Key"];
            string partitionKey = configurationSection["PartitionKey"];

            CosmosClient client = new CosmosClient(account, key);
            CosmosDbService<T> cosmosDbService = new CosmosDbService<T>(client, databaseName, containerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, partitionKey);

            return cosmosDbService;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Visitor Security Clearance API V1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
