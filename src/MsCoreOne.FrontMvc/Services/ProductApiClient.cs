using MsCoreOne.FrontMvc.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsCoreOne.FrontMvc.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _client;

        public ProductApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ProductVm> GetProductById(int productId)
        {
            var response = await _client.GetAsync($"/api/products/{productId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<IEnumerable<ProductVm>> GetProducts()
        {
            var response = await _client.GetAsync("/api/products");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

        public async Task<IEnumerable<ProductVm>> GetProductsByCategoryId(int categoryId)
        {
            var response = await _client.GetAsync($"/api/products/categories/{categoryId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
    }
}
