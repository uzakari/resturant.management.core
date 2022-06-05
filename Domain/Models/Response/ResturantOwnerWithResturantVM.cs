using Domain.Entity;

namespace Domain.Models.Response;

public class ResturantOwnerWithResturantVM 
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
    public List<ResturantVM> Resturants { get; set; }
}
