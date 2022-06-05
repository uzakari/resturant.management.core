using Application.Mappings.Resturant;
using Application.Repositories.ResturantApp.Resturant.Interface;
using AutoMapper;
using Domain.Entity;
using Domain.Models.Request;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTest.Mocks.Repository.Resturant;
using Xunit;

namespace UnitTest.Commands.CreateResturant;

public class CreateResturantTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IResturantRepository> _mockResturantRepository;
    public CreateResturantTest()
    {
        _mockResturantRepository = ResturantMocks.GetResturantRepository();
        var configurationProvider = new MapperConfiguration(act => act.AddProfile<ResturantMapping>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task GetAll_ResturantAndAvalableSeat_Success()
    {
        var resturantRepo = _mockResturantRepository.Object;
        var allResturantAndAvailbleSeat = await resturantRepo.GetAllResturantAndAvalableSeat();

        allResturantAndAvailbleSeat.Count.ShouldBe(1);

        allResturantAndAvailbleSeat.ShouldBeOfType<List<Resturant>>();
    }

    [Fact]
    public async Task CreateResturant_Success()
    {
        var resturantRepo = _mockResturantRepository.Object;
        var resturantToCreate = new ResturantDto()
        {
            Name = "test",
            ResturantOwnerEmail = "uzakari2@gmail.com",
            ResturantTables = new List<ResturantTableDto>(){
                new ResturantTableDto
                {
                    Available = true,
                    Location = "Rear"
                }
            }
        };
        var allResturantAndAvailbleSeat = await resturantRepo.CreateResturant(resturantToCreate);
        allResturantAndAvailbleSeat.ShouldNotBeNull();
        allResturantAndAvailbleSeat.ShouldBeOfType<Domain.Models.Response.ResturantVM>();
    }
}
