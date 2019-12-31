using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
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