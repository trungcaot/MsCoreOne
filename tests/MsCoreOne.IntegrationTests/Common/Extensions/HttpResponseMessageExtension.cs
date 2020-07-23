using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsCoreOne.IntegrationTests.Common.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> BodyAs<T>(this HttpResponseMessage httpResponseMessage)
        {
            var bodyString = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(bodyString);
        }
    }
}
