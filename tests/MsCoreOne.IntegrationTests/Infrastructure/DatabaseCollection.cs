using Xunit;

namespace MsCoreOne.IntegrationTests.Infrastructure
{
    [CollectionDefinition(nameof(DatabaseCollection))]
    public class DatabaseCollection : ICollectionFixture<BaseAppTestFixture>
    {
    }
}
