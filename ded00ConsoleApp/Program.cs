using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.RollingFileAlternate;

namespace ded00ConsoleApp
{
    class Program
    {
        public static IContainer Container { get; set; }
        static int Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            #region 設定Serilog
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .WriteTo.RollingFileAlternate("Log_Data", LogEventLevel.Warning)
               .CreateLogger();
            #endregion
            try
            {
                Log.Information("Starting console app");
                // Startup.cs finally :)
                Startup startup = new Startup();
                startup.ConfigureServices(services);
                //使用
                using (var scope = Container.BeginLifetimeScope())
                {
                    var batchService = scope.Resolve<IBatchService>();
                    batchService.WriteInformation("Injected!");
                }

                Console.WriteLine("Hello World!");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
