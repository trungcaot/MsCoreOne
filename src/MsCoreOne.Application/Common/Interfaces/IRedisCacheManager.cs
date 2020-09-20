using System.Threading.Tasks;

namespace MsCoreOne.Application.Common.Interfaces
{
    public interface IRedisCacheManager
    {
        Task<T> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T value);
    }
}
