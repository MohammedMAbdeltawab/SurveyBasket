using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Services;

public class MacOsService : IOperationTransient, IOperationScoped, IOperationSingleton
{
    public string OperationId { get; }

    public MacOsService()
    {
        OperationId = Guid.NewGuid().ToString();
    }

    public string RunApp() => "Running From Mac";
}
