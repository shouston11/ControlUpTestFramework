using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace ControlUpAPITestFramework.HttpHelpers
{
    public class InitialiseHttpRequest
    {
        public static async Task<string> InitiateRequest(HttpMethod method, string uri = null, string accessToken = null,
           string jsonRequest = null, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, Dictionary<string, string> formContent = null)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(uri),
                Headers =
            {
                { "X-RapidAPI-Key", accessToken },
                { "X-RapidAPI-Host", "binance43.p.rapidapi.com" },
            },
            };
                if (jsonRequest != null)
                {
                    request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                }

                else if (formContent != null)
                {
                    request.Content = new FormUrlEncodedContent(formContent);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                }
                var response = await client.SendAsync(request).ConfigureAwait(false);
                var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.StatusCode != expectedStatusCode)
                {
                    throw new WebException($"Request failed. Status code: {response.StatusCode}. JSON: {jsonResponse}");
                }

                return jsonResponse;
            }
        }
    }
