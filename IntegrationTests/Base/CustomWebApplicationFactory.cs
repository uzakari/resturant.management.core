using Domain.Models.Response.Authentication;
using IntegrationTests.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTests.Base;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public ServiceProvider ServiceProvider { get; private set; }
    public CustomWebApplicationFactory()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", false, true);

        var configuration = builder.Build();
    }  

    public async Task<HttpClient> GetAuthClient()
    {
        var client =  CreateClient();
        var loginResponse = await GetValidLoginResponse();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.AccessToken);
        return client;
    }

    public async Task<LoginResponse> GetValidLoginResponse()
    {
        var loginInput = TestData.GetLoginPayLoad();
        var client = CreateClient();

        var response = await client.PostAsJsonAsync("login", loginInput);
        response.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync());
    }


}
