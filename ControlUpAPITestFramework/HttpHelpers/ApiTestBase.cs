using ControlUpAPITestFramework.JsonHelpers;
using System.Net;

namespace ControlUpAPITestFramework.HttpHelpers
{
    public class ApiTestBase
    {
        public static async Task<List<T>> MakeGetRequestAsyncList<T>(string uri, string accessToken = null, HttpStatusCode expectedStatusCode = HttpStatusCode.OK,
               bool validateSchema = false, string schemaFields = null)
        {
            var jsonResponse = await MakeApiCallAsync(uri, HttpMethod.Get, accessToken, expectedStatusCode: expectedStatusCode, schemaFields: schemaFields,
                validateSchema: validateSchema).ConfigureAwait(false);

            return jsonResponse.FromJsonList<T>(JsonExtensions.DefaultSettings);

        }

        public static async Task<T> MakeGetRequestAsync<T>(string uri, string accessToken = null, HttpStatusCode expectedStatusCode = HttpStatusCode.OK,
           bool validateSchema = false, string schemaFields = null)
        {
            var jsonResponse = await MakeApiCallAsync(uri, HttpMethod.Get, accessToken, expectedStatusCode: expectedStatusCode, schemaFields: schemaFields,
                validateSchema: validateSchema).ConfigureAwait(false);

            return jsonResponse.FromJson<T>(JsonExtensions.DefaultSettings);
        }

        public static async Task<string> MakeApiCallAsync(string uri, HttpMethod method, string accessToken, string jsonRequest = null,
               HttpStatusCode expectedStatusCode = HttpStatusCode.OK, bool validateSchema = false, string schemaFields = null, Dictionary<string, string> formContent = null)
        {
            var jsonResponse = await InitialiseHttpRequest.InitiateRequest(method: method, uri: uri, accessToken: accessToken,
                jsonRequest: jsonRequest, expectedStatusCode: expectedStatusCode, formContent: formContent);

            if (validateSchema == true)
            {
                AssertJsonSchema.Valid(schemaFields, jsonResponse.ToString());
            }

            return jsonResponse;
        }
    }
}
