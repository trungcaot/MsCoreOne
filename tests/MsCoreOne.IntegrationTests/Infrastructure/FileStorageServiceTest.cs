using Microsoft.AspNetCore.Hosting;
using MsCoreOne.Application.Common.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace MsCoreOne.IntegrationTests.Infrastructure
{
    public class FileStorageServiceTest : IStorageService
    {
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public Task DeleteFileAsync(string fileName)
        {
            return Task.CompletedTask;
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            return Task.CompletedTask;
        }
    }
}
