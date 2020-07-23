using MsCoreOne.Application.Products.Commands.CreateProduct;
using MsCoreOne.Application.Products.Commands.UpdateProduct;
using MsCoreOne.Domain.Entities;
using MsCoreOne.IntegrationTests.Common;
using MsCoreOne.IntegrationTests.Common.Extensions;
using MsCoreOne.IntegrationTests.Common.Utility.TestCaseOrdering;
using MsCoreOne.IntegrationTests.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MsCoreOne.IntegrationTests.TestScenarios
{
    [Collection(nameof(DatabaseCollection))]
    [TestCaseOrderer(Constants.OrdererTypeName, Constants.OrdererAssemblyName)]
    public class ProductScenarios : BaseScenarios
    {
        private readonly string _url = "api/products";

        public ProductScenarios(BaseAppTestFixture fixture)
            : base(fixture) { }

        [Fact, Priority(0)]
        public async Task Post_Success()
        {
            // Act
            HttpResponseMessage response;

            //using (var file = File.OpenRead(@"Common\Images\test.jpg"))
            //using (var content = new StreamContent(file))
            using (var formData = new MultipartFormDataContent())
            {
                //formData.Add(content, "file", "test.jpg");
                formData.Add(new StringContent("Product 1"), "name");
                formData.Add(new StringContent("999"), "price");
                formData.Add(new StringContent("1"), "categoryId");
                formData.Add(new StringContent("1"), "brandId");

                response = await HttpClient.PostAsync(_url, formData);
            }

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }

        [Fact, Priority(1)]
        public async Task Put_Success()
        {
            // Act
            HttpResponseMessage response;

            //using (var file = File.OpenRead(@"Common\Images\test.jpg"))
            //using (var content = new StreamContent(file))
            using (var formData = new MultipartFormDataContent())
            {
                //formData.Add(content, "file", "test.jpg");
                formData.Add(new StringContent("1"), "id");
                formData.Add(new StringContent("Product name changed"), "name");
                formData.Add(new StringContent("899"), "price");
                formData.Add(new StringContent("1"), "categoryId");
                formData.Add(new StringContent("1"), "brandId");

                response = await HttpClient.PutAsync(_url, formData);
            }

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }

        [Fact, Priority(3)]
        public async Task GetById_Success()
        {
            // Arrange
            int productId = 1;

            // Act
            var response = await HttpClient.GetAsync($"{_url}/{productId}");
            var product = await response.BodyAs<Product>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(product);
        }

        [Fact, Priority(4)]
        public async Task Get_Success()
        {
            // Act
            var response = await HttpClient.GetAsync(_url);
            var products = await response.BodyAs<IEnumerable<Product>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(products);
        }

        [Fact, Priority(5)]
        public async Task Delete_Success()
        {
            // Arrange
            HttpResponseMessage responseCreateProduct;
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("Product 3"), "name");
                formData.Add(new StringContent("399"), "price");
                formData.Add(new StringContent("1"), "categoryId");
                formData.Add(new StringContent("1"), "brandId");

                responseCreateProduct = await HttpClient.PostAsync(_url, formData);
            }
            var productId = await responseCreateProduct.Content.ReadAsStringAsync();

            // Act
            var response = await HttpClient.DeleteAsync($"{_url}/{productId}");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }
    }
}
