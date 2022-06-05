namespace Domain.Entity;

public class ResturantOwner
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
    public ICollection<Resturant> Resturants { get; set; } = new List<Resturant>();
}
