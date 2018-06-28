using Autofac;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ded00ConsoleApp
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }
        
        public Startup()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
           
        }
        internal void ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            //註冊log
            builder.RegisterLogger();
            //將service註冊
            builder.Register(x => this.Configuration).As<IConfigurationRoot>();
            builder.RegisterType<BatchService>().As<IBatchService>();

            Program.Container = builder.Build();
        }
    }
}
