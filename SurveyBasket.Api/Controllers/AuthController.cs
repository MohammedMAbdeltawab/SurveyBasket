using Microsoft.AspNetCore.Authorization;

namespace SurveyBasket.Api.Controllers;

[Route("[controller]")]
[ApiController]
[AllowAnonymous] // login must stay public even when other controllers use [Authorize]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("")]
    public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
        return authResult is null ? BadRequest("Invalid email/password") : Ok(authResult);
    }

   

}

