using Application.Commands.Resturant.CreateResturant;
using Application.Queries.Resturant.GetAllResturantWithAvailableTable;
using Domain.Models.Request;
using Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.Resturant;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ResturantController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResturantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ResturantVM>>> GetAvailableResturants()
    {
        return Ok(await _mediator.Send(new GetAllResturantWithAvailableTableQuery()));
    }


    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResturantVM>> CreateResturant([FromBody] ResturantDto resturant)
    {
        return Ok(await _mediator.Send(new CreateResturantCommand(resturant)));
    }
}
