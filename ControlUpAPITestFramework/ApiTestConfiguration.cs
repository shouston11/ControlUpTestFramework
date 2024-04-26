using ControlUpAPITestFramework.Models.API;
using Microsoft.Extensions.Configuration;

namespace ControlUpAPITestFramework
{
    public static class ApiTestConfiguration
    {
        private static IConfigurationRoot _config;
        public static ApiTestConfigurationModel Get()
        {
            if (_config == null)
                _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return _config.GetSection("ApiTest").Get<ApiTestConfigurationModel>();
        }
    }
}