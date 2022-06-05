using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Request;

public class ResturantTableDto
{
    public int NumberOfSeat { get; set; }
    public string? Location { get; set; }
    public bool Available { get; set; }
}
