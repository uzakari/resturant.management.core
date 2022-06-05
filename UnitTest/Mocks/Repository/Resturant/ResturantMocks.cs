using Application.Repositories.ResturantApp.Resturant.Interface;
using Domain.Entity;
using Domain.Models.Request;
using Moq;
using System.Collections.Generic;

namespace UnitTest.Mocks.Repository.Resturant;

public class ResturantMocks
{
    public static Mock<IResturantRepository> GetResturantRepository()
    {
        var resturants = new List<Domain.Entity.Resturant>() { new Domain.Entity.Resturant {
            Id = 1,
            Name = "Fresh View",
            Owner = new ResturantOwner
            {
                FirstName = "umar"
            },
            ResturantOwnerId = 1,
                ResturantTables = new List<Domain.Entity.ResturantTable>()
                {
                    new Domain.Entity.ResturantTable {
                        Available = true,
                        Id = 1,
                        Location = "Front",
                        NumberOfSeat = 8
                    }
                }
            }
            
        };
        var mockResturantRepository = new Mock<IResturantRepository>();
        mockResturantRepository.Setup(repo => repo.GetAllResturantAndAvalableSeat()).ReturnsAsync(resturants);
        mockResturantRepository.Setup(repo => repo.CreateResturant(It.IsAny<ResturantDto>()))
            .ReturnsAsync((Domain.Models.Response.ResturantVM newResturant) =>  newResturant );


        return mockResturantRepository;
    }
}
