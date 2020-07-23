using MsCoreOne.FrontMvc.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsCoreOne.FrontMvc.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly HttpClient _client;

        public CategoryApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CategoryVm>> GetCategories()
        {
            var response = await _client.GetAsync("/api/categories");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<CategoryVm>>();
        }
    }
}
