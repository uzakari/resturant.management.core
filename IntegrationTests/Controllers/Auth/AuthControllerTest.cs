using Domain.Models.Request;
using Domain.Models.Response;
using IntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Controllers.Auth;

public class AuthControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _customWebApplicationFactory;
    private readonly HttpClient _client;


    public AuthControllerTest(CustomWebApplicationFactory<Program> customWebApplicationFactory)
    {
        customWebApplicationFactory.ClientOptions.BaseAddress = new Uri("https://localhost:7091/api/auth/");
        _customWebApplicationFactory = customWebApplicationFactory;
        _client = customWebApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task CreateResturantOwner_Success()
    {
        var inputPayload = new ResturantOwnerDto
        {
            DateOfBirth = DateTimeOffset.Now.AddYears(-23),
            Email = "uzakari4@gmail.com",
            FirstName = "umar",
            LastName = "Isa",
            Password = "test"
        };

        var resturantOwnerCreationResponse = await _client.PostAsJsonAsync("signUp", inputPayload);
        resturantOwnerCreationResponse.EnsureSuccessStatusCode();

        var responseContent = await resturantOwnerCreationResponse.Content.ReadAsStringAsync();
        responseContent.ShouldNotBeNull();
        var returnData = JsonConvert.DeserializeObject<ResturantOwnerVM>(responseContent);
        returnData.ShouldBeOfType<ResturantOwnerVM>();
    }

    [Fact]
    public async Task GetAllResturantOwner_Success()
    {
        var authClient = await _customWebApplicationFactory.GetAuthClient();
        var resturantOwnerResponse = await authClient.GetAsync("users");
        resturantOwnerResponse.EnsureSuccessStatusCode();

        var respnseContent = await resturantOwnerResponse.Content.ReadAsStringAsync();
        respnseContent.ShouldNotBeNull();
        var toReturn = JsonConvert.DeserializeObject<List<ResturantOwnerWithResturantVM>>(respnseContent);
        toReturn.ShouldNotBeNull();
        toReturn.ShouldBeOfType<List<ResturantOwnerWithResturantVM>>();
    }

}
