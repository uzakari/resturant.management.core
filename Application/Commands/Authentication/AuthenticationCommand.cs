using Application.Repositories.ResturantApp.ResturantOwner.Interface;
using Domain.Constants;
using Domain.Exception;
using Domain.Extension;
using Domain.Models.Request.Authentication;
using Domain.Models.Response.Authentication;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Commands.Authentication;

public record AuthenticationCommand(LoginRequest loginRequest) : IRequest<LoginResponse>;


public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, LoginResponse>
{
    private readonly JwtSettings _jwtSettings;
    private readonly IResturantOwnerRepository _resturantOwnerRepository;

    public AuthenticationCommandHandler(IOptions<JwtSettings> options, IResturantOwnerRepository resturantOwnerRepository)
    {
        _jwtSettings = options.Value;
        _resturantOwnerRepository = resturantOwnerRepository;
    }
    public async Task<LoginResponse> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        var jwtSecurityTokken = new JwtSecurityToken();

        var getResturantOwner = await _resturantOwnerRepository.GetResturantOwner(request.loginRequest.Email);

        if (getResturantOwner == null)
            throw new NotFoundException(nameof(AuthenticationCommandHandler), nameof(AuthenticationCommand));

        if (getResturantOwner.Password != request.loginRequest.Password.HashedPassword())
            throw new ResturantValidationException("Invalid Credentials");


        jwtSecurityTokken = await GenerateToken(getResturantOwner);

        return new LoginResponse(GetTokenString(jwtSecurityTokken), ExpiresIn(jwtSecurityTokken), ResturantConstants.TokenType);

    }



    private async Task<JwtSecurityToken> GenerateToken(Domain.Entity.ResturantOwner user)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.LastName.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", $"{user.FirstName}{user.LastName}")
         };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    private static string GetTokenString(JwtSecurityToken jwtSecurityTokken)
    {
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityTokken);
    }

    private static int ExpiresIn(JwtSecurityToken jwtSecurityTokken)
    {
        return (int)(jwtSecurityTokken.ValidTo - DateTime.Now).TotalSeconds;
    }

}
