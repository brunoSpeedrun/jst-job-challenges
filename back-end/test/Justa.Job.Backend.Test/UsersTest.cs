using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Identity.Models;
using Newtonsoft.Json;
using Xunit;

namespace Justa.Job.Backend.Test
{
    public class UsersTest : JustaJobBackendTestBase
    {
        [Fact]
        public async Task ShouldCreateUser()
        {
            var jwt = await GetJwt();

            var fakerUser = new Faker<CreateUserRequest>()
                                    .RuleFor(x => x.UserName, f => f.Person.UserName)
                                    .RuleFor(x => x.Email, f => f.Person.Email)
                                    .RuleFor(x => x.Password, f => NewPassword());

            var newUser = fakerUser.Generate(1).FirstOrDefault();

            var stringContent = ToStringContentApplicationJson(newUser);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            using (var httpResponse = await _httpClient.PostAsync("/users", stringContent))
            {
                Assert.True(httpResponse.StatusCode == HttpStatusCode.Created);
                Assert.True(httpResponse.Headers.Contains("Location"));

                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                var userCreated = JsonConvert.DeserializeObject<CreateUserRequest>(httpContent);
                
                Assert.Equal(userCreated.UserName, newUser.UserName);
            }
        }

        [Fact]
        public async Task ShouldGetUser()
        {
            var jwt = await GetJwt();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            using (var httpResponse = await _httpClient.GetAsync($"/users/{_adminUserName}"))
            {
                Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);
                
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                var applicationUser = JsonConvert.DeserializeObject<ApplicationUser>(httpContent);
                
                Assert.Equal(_adminUserName, applicationUser.UserName);
            }
        }

        [Fact]
        public async Task ShouldGetUsers()
        {
            var jwt = await GetJwt();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            var queryString = $"page=0&pageSize=10&sortBy=email";

            using (var httpResponse = await _httpClient.GetAsync($"/users?{queryString}"))
            {
                Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);
                
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                Assert.False(string.IsNullOrEmpty(httpContent));
            }
        }

        [Fact]
        public async Task ShouldDeleteUser()
        {
            var jwt = await GetJwt();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            using (var httpResponse = await _httpClient.GetAsync($"/users"))
            {
                Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);
                
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                var applicationUsers = JsonConvert.DeserializeObject<ApplicationUser[]>(httpContent);

                var userToDelete = applicationUsers.FirstOrDefault(u => u.UserName != _adminUserName);

                if (userToDelete != null)
                {
                    using (var httpResponseDeleteUser = await _httpClient.DeleteAsync($"/users/{userToDelete.UserName}"))
                    {
                        Assert.True(httpResponseDeleteUser.StatusCode == HttpStatusCode.NoContent);
                    }       
                }
            }
        }

        public string NewPassword()
        {
            var upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lowerCase = "abcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";
            var nonAlphanumeric = "@!#$";

            var tamanho = 6;

            var random = new Random(DateTime.Now.Millisecond);

            var builder = new StringBuilder(tamanho);

            builder.Append(upperCase[random.Next(0, 25)]);
            builder.Append(numbers[random.Next(0, 9)]);
            builder.Append(numbers[random.Next(0, 9)]);
            builder.Append(numbers[random.Next(0, 9)]);
            builder.Append(nonAlphanumeric[random.Next(0, 3)]);
            builder.Append(lowerCase[random.Next(0, 25)]);

            var senha = builder.ToString();
            return senha;
        }
    }
}