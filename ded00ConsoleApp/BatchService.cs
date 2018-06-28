using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ded00ConsoleApp
{
    public interface IBatchService
    {
        void WriteInformation(string input);
    }

    public class BatchService : IBatchService
    {
        private readonly string _baseUrl;
        private readonly string _token;
        readonly ILogger _logger;
        public BatchService(IConfigurationRoot config,ILogger logger)
        {
            var baseUrl = config["SomeConfigItem:BaseUrl"];
            var token = config["SomeConfigItem:Token"];
            _baseUrl = baseUrl;
            _token = token;
            _logger = logger;
        }
        public void WriteInformation(string input)
        {
            Console.WriteLine(input);
            Console.WriteLine(_baseUrl);
            Console.WriteLine(_token);
            _logger.Information("just log test");
            Console.ReadKey();
        }
    }
}
