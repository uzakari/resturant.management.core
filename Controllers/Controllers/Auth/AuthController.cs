using Application.Commands.Authentication;
using Application.Commands.ResturantOwner.CreateResturantOwner;
using Application.Queries.ResturantOwner;
using Domain.Models.Request;
using Domain.Models.Request.Authentication;
using Domain.Models.Response;
using Domain.Models.Response.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediar;

    public AuthController(IMediator mediar)
    {
        _mediar = mediar;
    }

    [HttpGet("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ResturantOwnerWithResturantVM>>> GetAllResturantOwner()
    {
        return Ok(await _mediar.Send(new ResturantOwnerQuery()));
    }

    [HttpPost("signUp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResturantOwnerVM>> CreateResturantOwner([FromBody] ResturantOwnerDto resturantOwnerDto)
    {
        var restrurantCreatedResponse = await _mediar.Send(new CreateResturantOwnerCommand(resturantOwnerDto));
        return CreatedAtRoute(restrurantCreatedResponse, new { restrurantCreatedResponse });
    }


    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        return Ok(await _mediar.Send(new AuthenticationCommand(loginRequest)));
    }
}
