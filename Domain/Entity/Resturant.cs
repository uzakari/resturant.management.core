namespace Domain.Entity;

public class Resturant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ResturantOwner Owner { get; set; }
    public int ResturantOwnerId { get; set; }
    public ICollection<ResturantTable> ResturantTables { get; set; } = new List<ResturantTable>();
}
