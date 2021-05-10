using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;


namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedData(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }

            await host.RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // .ConfigureAppConfiguration((hostingContext, config) =>
                // {
                //     config.AddSystemsManager($"/db", TimeSpan.FromMinutes(1));
                // })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(((context, builder) =>
                {
                    var environmentName = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development").ToLower();

                    builder.AddJsonFile("appsettings.json");
                    builder.AddEnvironmentVariables();

                    AWSOptions awsOptions = null;

                    if (Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") != null)
                    {
                        awsOptions = new AWSOptions();
                        awsOptions.Region = RegionEndpoint.USWest2;
                        awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
                    }

                    builder.AddSystemsManager((source) =>
                    {
                        source.Path = $"/db";
                        source.AwsOptions = awsOptions;
                    });
                }));
    }
}
