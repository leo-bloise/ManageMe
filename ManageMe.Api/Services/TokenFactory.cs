using ManageMe.Api.Options;
using ManageMe.Application;
using ManageMe.Application.Services;
using ManageMe.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageMe.Api.Services;

public class TokenFactory(IOptions<JwtSettings> options) : ITokenFactory
{
    private SymmetricSecurityKey Secret => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value?.Secret ?? ""));

    private SigningCredentials Credentials => new SigningCredentials(Secret, SecurityAlgorithms.HmacSha256);

    public Token Create(User user)
    {
        ArgumentNullException.ThrowIfNull(options.Value);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: GenerateClaims(user),
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: Credentials
        );

        return new Token(new JwtSecurityTokenHandler().WriteToken(token));
    }

    private Claim[] GenerateClaims(User user)
    {
        return [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        ];
    }
}
