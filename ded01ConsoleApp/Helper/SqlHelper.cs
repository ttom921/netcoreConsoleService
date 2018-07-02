using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper
{
    #region SqlHelper
    public static class SqlHelper
    {
        #region Connection String & Timeout
        private static IConfigurationRoot config;
        public static IConfigurationRoot Config
        {
            get
            {
                if (config == null)
                    config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                return config;
            }
        }

        public static string GetConnectionString(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                return Config["ConnectionStrings:GomoDatabase"];
            else
                return connectionString;
        }

        public static int ConnectionTimeout { get; set; }

        public static int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }
        #endregion //Connection String & Timeout
    }
    #endregion //SqlHelper
}
