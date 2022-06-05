using Domain.Entity;

namespace Domain.Models.Request;

public class ResturantDto
{
    public string Name { get; set; }
    public string ResturantOwnerEmail { get; set; }
    public List<ResturantTableDto> ResturantTables { get; set; }
}
