namespace Domain.Entity;

public class ResturantTable
{
    public int Id { get; set; }
    public int NumberOfSeat { get; set; }
    public string? Location { get; set; }
    public bool Available { get; set; }
}
