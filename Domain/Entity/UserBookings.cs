namespace Domain.Entity;

public class UserBookings
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int ResturantID { get; set; }
    public int ResturantTableID { get; set; }
    public int NumberOfPersons { get; set; }
    public DateTimeOffset ReservationTime { get; set; } = DateTimeOffset.Now;
}
