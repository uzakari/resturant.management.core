namespace Domain.Models.Response;

public class ResturantTableVM
{
    public int Id { get; set; }
    public int NumberOfSeat { get; set; }
    public string? Location { get; set; }
    public bool Available { get; set; }
}
