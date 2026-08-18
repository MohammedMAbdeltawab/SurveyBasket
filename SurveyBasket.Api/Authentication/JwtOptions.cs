using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Api.Authentication;

public class JwtOptions
{
    public static string SectionName = "Jwt"; // the name in the appsettings to map it in Depencey injections configurations
    [Required]
    public string Key { get; init; } = string.Empty;
    [Required]
    public string Issuer { get; init; } = string.Empty;
    [Required]
    public string Audience { get; init; } = string.Empty;
    [Range(1, int.MaxValue)]
    public int ExpiryMinutes { get; set; }
}
