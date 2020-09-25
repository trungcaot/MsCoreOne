using MsCoreOne.Domain.Entities;
using MsCoreOne.IntegrationTests.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MsCoreOne.IntegrationTests.TestScenarios
{
    [Collection(nameof(DatabaseCollection))]
    public class BrandScenarios : BaseScenarios
    {
        private readonly string _url = "api/brands";

        public BrandScenarios(BaseAppTestFixture fixture)
            : base(fixture) { }

        [Fact]
        public async Task Get_Success()
        {
            // Act
            var response = await HttpClient.GetAsync(_url);
            var brands = await response.Content.ReadAsAsync<IEnumerable<Brand>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(brands);
        }
    }
}
