using Domain.Entity;

namespace Domain.Models.Response;

public class ResturantVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ResturantOwnerVM Owner { get; set; }
    public int ResturantOwnerId { get; set; }
    public ICollection<ResturantTableVM> ResturantTables { get; set; }
}
