using MsCoreOne.Application.Categories.Commands.CreateCategory;
using MsCoreOne.Application.Categories.Commands.UpdateCategory;
using MsCoreOne.Application.Categories.Queries.Dtos;
using MsCoreOne.Application.Common.Models;
using MsCoreOne.IntegrationTests.Common;
using MsCoreOne.IntegrationTests.Common.Extensions;
using MsCoreOne.IntegrationTests.Common.Utility.TestCaseOrdering;
using MsCoreOne.IntegrationTests.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MsCoreOne.IntegrationTests.TestScenarios
{
    [Collection(nameof(DatabaseCollection))]
    [TestCaseOrderer(Constants.OrdererTypeName, Constants.OrdererAssemblyName)]
    public class CategoryScenarios : BaseScenarios
    {
        private readonly string _url = "api/categories";

        public CategoryScenarios(BaseAppTestFixture fixture)
            : base(fixture) { }

        [Fact, Priority(0)]
        public async Task Post_Success()
        {
            // Arrange
            var categoryDto = new CreateCategoryDto { Name = "Category 1" };

            // Act
            var response = await HttpClient.PostAsJsonAsync(_url, categoryDto);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(int.Parse(result) == 2);
        }

        [Fact]
        public async Task Put_Success()
        {
            // Arrange
            var categoryDto = new UpdateCategoryDto { 
                Id = 1, 
                Name = "Category 2", 
                Original = new CategoryDto
                { 
                    Id = 1,
                    Name = "Category"
                }
            };

            // Act
            var response = await HttpClient.PutAsJsonAsync(_url, categoryDto);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_Success()
        {
            // Arrange
            int categoryId = 1;

            // Act
            var response = await HttpClient.GetAsync($"{_url}/{categoryId}");
            var category = await response.BodyAs<CategoryDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(category);
        }

        [Fact]
        public async Task Get_Success()
        {
            // Act
            var pagedUrl = $"{_url}?pageNumber=1&pageSize=10";
            var response = await HttpClient.GetAsync(pagedUrl);
            var categories = await response.BodyAs<PagedResponse<IEnumerable<CategoryDto>>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(categories);
        }

        [Fact, Priority(1)]
        public async Task Delete_Success()
        {
            // Arrange
            int categoryId = 2;

            // Act
            var response = await HttpClient.DeleteAsync($"{_url}/{categoryId}");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }
    }
}
