using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyBasket.Api.Authentication;

public class JwtProvider : IJwtProvider
{
    public (string Token, int expiresIn) GenerateToken(ApplicationUser user)
    {
        Claim[] claims = [
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (JwtRegisteredClaimNames.GivenName, user.FirstName),
            new (JwtRegisteredClaimNames.FamilyName, user.LastName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ];

        var symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("NFtAg3nkfuq9WuvAVn3fu8c6PuzUnYmc"));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expiresIn = 30;
        var expirationDate = DateTime.UtcNow.AddMinutes(expiresIn);

        var token = new JwtSecurityToken(
            issuer: "SurveyBasket",
            audience: "SurveyBasket Users",
            claims: claims,
            signingCredentials: signingCredentials,
            expires: expirationDate
        );

        return (Token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: expiresIn);
    }
}
