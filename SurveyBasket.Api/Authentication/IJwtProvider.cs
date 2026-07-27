namespace SurveyBasket.Api.Authentication;

public interface IJwtProvider
{
    (string Token , int expiresIn) GenerateToken(ApplicationUser user);
}
