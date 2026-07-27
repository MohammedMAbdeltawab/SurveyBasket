namespace SurveyBasket.Api.Services.Authentication;

public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        // 1- Check the user exists
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return null;

        // 2- Check the password
        var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
        if (!isValidPassword)
            return null;

        // 3- Generate the JWT token
        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        // 4- Return the response
        return new AuthResponse(user.Id, user.Email!, user.FirstName, user.LastName, token, expiresIn);
    }
}
