using Application.Commands.UserBookings;
using Domain.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.UserBooking;

[Route("api/[controller]")]
[ApiController]
public class UserBookingController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserBookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("book")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BookResturant([FromBody] UserBookingDto userBookingDto)
    {
       return Ok(await _mediator.Send(new UserBookingsCommand<UserBookingDto>(userBookingDto)));
    }
}
