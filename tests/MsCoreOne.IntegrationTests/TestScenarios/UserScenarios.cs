using MsCoreOne.Application.Common.Models;
using MsCoreOne.Application.Users.Commands.CreateUser;
using MsCoreOne.Application.Users.Commands.UpdateUser;
using MsCoreOne.Application.Users.Share;
using MsCoreOne.IntegrationTests.Common;
using MsCoreOne.IntegrationTests.Common.Extensions;
using MsCoreOne.IntegrationTests.Common.Utility.TestCaseOrdering;
using MsCoreOne.IntegrationTests.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MsCoreOne.IntegrationTests.TestScenarios
{
    [Collection(nameof(DatabaseCollection))]
    [TestCaseOrderer(Constants.OrdererTypeName, Constants.OrdererAssemblyName)]
    public class UserScenarios : BaseScenarios
    {
        private readonly string _url = "api/users";

        public UserScenarios(BaseAppTestFixture fixture)
            : base(fixture) { }

        [Fact, Priority(0)]
        public async Task Post_Success()
        {
            // Arrange
            var userDto = new CreateUserDto 
            {
                FirstName = "Test",
                LastName = "Test",
                UserName = "test@test.com",
                Password = "P@ssw0rd"
            };

            // Act
            var response = await HttpClient.PostAsJsonAsync(_url, userDto);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Put_Success()
        {
            // Arrange
            var userDto = new CreateUserDto
            {
                FirstName = "Test",
                LastName = "Test",
                UserName = "test1@test.com",
                Password = "P@ssw0rd"
            };

            var responseCreateUser = await HttpClient.PostAsJsonAsync(_url, userDto);
            var userId = await responseCreateUser.Content.ReadAsStringAsync();
            
            var updateUserDto = new UpdateUserDto
            {
                Id = userId,
                FirstName = "test 2",
                LastName = "test 2",
                UserName = "test2@test.com"
            };

            // Act
            var response = await HttpClient.PutAsJsonAsync(_url, updateUserDto);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result, userId);
        }

        [Fact]
        public async Task Delete_Success()
        {
            // Arrange
            var userDto = new CreateUserDto
            {
                FirstName = "Test 3",
                LastName = "Test 3",
                UserName = "test3@test.com",
                Password = "P@ssw0rd"
            };

            var responseCreateUser = await HttpClient.PostAsJsonAsync(_url, userDto);
            var userId = await responseCreateUser.Content.ReadAsStringAsync();

            // Act
            var response = await HttpClient.DeleteAsync($"{_url}/{userId}");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get_Success()
        {
            // Act
            var response = await HttpClient.GetAsync(_url);
            var brands = await response.BodyAs<IEnumerable<UserDto>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(brands);
        }
    }
}
