using Microsoft.AspNetCore.Authorization;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class TestController(IOptions<JwtOptions> jwtOptions, IConfiguration configuration) : ControllerBase
{
    /// <summary>Section 08 — Options Pattern: strongly typed Jwt settings via IOptions.</summary>
    [HttpGet("jwt")]
    public IActionResult Jwt() => Ok(jwtOptions.Value);

    /// <summary>Section 08 — IConfiguration: read flat keys, nested keys (:), env vars.</summary>
    [HttpGet("config")]
    public IActionResult Config() => Ok(new
    {
        MyKey = configuration["MyKey"],
        ParentChild = configuration["Parent:Child"],
        LogLevel = configuration["Logging:LogLevel:Default"],
        Environment = configuration["ASPNETCORE_ENVIRONMENT"],
        ConnectionString = configuration.GetConnectionString("DefaultConnection") is not null
            ? "(configured)"
            : "(missing)"
    });
}
