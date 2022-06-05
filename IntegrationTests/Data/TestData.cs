
using Domain.Models.Request.Authentication;

namespace IntegrationTests.Data;

public static class TestData
{
    public static LoginRequest GetLoginPayLoad()
    {
        return new LoginRequest
        {
           Email = "emjay@gmail.com",
           Password = "test"
        };
    }
}
