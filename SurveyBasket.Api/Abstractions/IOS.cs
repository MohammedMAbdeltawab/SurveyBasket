namespace SurveyBasket.Api.Abstractions;

public interface IOS
{
    string RunApp();
    string OperationId { get; }
}

public interface IOperationTransient : IOS { }
public interface IOperationScoped : IOS { }
public interface IOperationSingleton : IOS { }
