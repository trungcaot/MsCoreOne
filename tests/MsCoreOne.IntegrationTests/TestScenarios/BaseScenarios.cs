using MsCoreOne.IntegrationTests.Infrastructure;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MsCoreOne.IntegrationTests.TestScenarios
{
    public abstract class BaseScenarios : BaseAppTestFixture
    {
        protected HttpClient HttpClient { get; set; }

        protected BaseAppTestFixture BaseAppTestFixture { get; set; }

        protected BaseScenarios(BaseAppTestFixture fixture)
        {
            BaseAppTestFixture = fixture;

            HttpClient = fixture.CreateClient();
        }
    }
}
