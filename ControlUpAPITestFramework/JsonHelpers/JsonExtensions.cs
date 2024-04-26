using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ControlUpAPITestFramework.JsonHelpers
{
    public static class JsonExtensions
    {
        public static readonly JsonSerializerSettings DefaultSettings = CreateSettings();

        public static JsonSerializerSettings CreateSettings()
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false,
                    ProcessDictionaryKeys = true
                }
            };

            var settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            return settings;
        }
        
        public static T FromJson<T>(this string json, JsonSerializerSettings settings)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Additional text encountered after finished reading JSON content"))
                {
                    throw new Exception($"Was not able to deserialize the following json: {json}");
                }
                throw;
            }
        }
        public static List<T> FromJsonList<T>(this string json, JsonSerializerSettings settings)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(json, settings);
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Additional text encountered after finished reading JSON content"))
                {
                    throw new Exception($"Was not able to deserialize the following json: {json}");
                }
                throw;
            }
        }
    }
}
