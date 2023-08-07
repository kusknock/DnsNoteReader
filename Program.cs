using DnsNoteWriter.Services;
using DnsNoteWriter.Services.Interfaces;
using DnsNoteWriter.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace DnsNoteWriter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                await services.GetRequiredService<App>().Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            IHostBuilder CreateHostBuilder(string[] strings)
            {
                return Host.CreateDefaultBuilder()
                    .ConfigureServices((_, services) =>
                    {
                        services.AddSingleton<App>();
                        services.AddSingleton<GatewayClient>();
                        services.AddScoped<INoteReader, NoteReader>();
                        services.AddScoped<NoteReaderService>();
                    })
                    .ConfigureAppConfiguration(app =>
                    {
                        app.AddJsonFile("appsettings.json");
                    });
            }
        }
    }
}