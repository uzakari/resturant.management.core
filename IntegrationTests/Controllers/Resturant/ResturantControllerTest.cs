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

namespace IntegrationTests.Controllers.Resturant;

public class ResturantControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _customWebApplicationFactory;
    private readonly HttpClient _client;

    public ResturantControllerTest(CustomWebApplicationFactory<Program> customWebApplicationFactory)
    {
        customWebApplicationFactory.ClientOptions.BaseAddress = new Uri("https://localhost:7091/api/resturant/");
        _customWebApplicationFactory = customWebApplicationFactory;
        _client = customWebApplicationFactory.CreateClient();
    }


    [Fact]
    public async Task GetAvailableResturants_Success()
    {
        var resturantOwnerResponse = await _client.GetAsync("available");
        resturantOwnerResponse.EnsureSuccessStatusCode();

        var respnseContent = await resturantOwnerResponse.Content.ReadAsStringAsync();
        respnseContent.ShouldNotBeNull();
        var toReturn = JsonConvert.DeserializeObject<List<ResturantOwnerWithResturantVM>>(respnseContent);
        toReturn.ShouldNotBeNull();
        toReturn.ShouldBeOfType<List<ResturantOwnerWithResturantVM>>();
    }

    [Fact]
    public async Task CreateResturant_Success()
    {
        var inputPayload = new ResturantDto
        {
            ResturantOwnerEmail = "nzakari@gmail.com",
            Name = "Suman Suite",
            ResturantTables = new List<Domain.Models.Request.ResturantTableDto>()
            {
                new Domain.Models.Request.ResturantTableDto
                {
                    Available = true,
                    Location = "Middle",
                    NumberOfSeat = 34
                }
            }
        };

        var authClient = await _customWebApplicationFactory.GetAuthClient();
        var resturantOwnerCreationResponse = await authClient.PostAsJsonAsync("create", inputPayload);
        resturantOwnerCreationResponse.EnsureSuccessStatusCode();

        var responseContent = await resturantOwnerCreationResponse.Content.ReadAsStringAsync();
        responseContent.ShouldNotBeNull();
        var returnData = JsonConvert.DeserializeObject<ResturantVM>(responseContent);
        returnData.ShouldBeOfType<ResturantVM>();
    }

}
