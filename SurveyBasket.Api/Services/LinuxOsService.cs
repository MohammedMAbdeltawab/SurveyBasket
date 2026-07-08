using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Services;

public class LinuxOsService : IOperationTransient, IOperationScoped, IOperationSingleton
{
    public string OperationId { get; }

    public LinuxOsService()
    {
        OperationId = Guid.NewGuid().ToString()[^4..];
    }

    public string RunApp() => "Running From Linux";
}
