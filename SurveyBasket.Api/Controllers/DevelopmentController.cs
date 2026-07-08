using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevelopmentController : ControllerBase
{
    private readonly ILogger<DevelopmentController> _logger;
    private readonly IOperationTransient _operationTransient;
    private readonly IOperationScoped _operationScoped;
    private readonly IOperationSingleton _operationSingleton;

    public DevelopmentController(
        IOperationTransient operationTransient,
        IOperationScoped operationScoped,
        IOperationSingleton operationSingleton,
        ILogger<DevelopmentController> logger)
    {
        _operationTransient = operationTransient;
        _operationScoped = operationScoped;
        _operationSingleton = operationSingleton;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Run(
        [FromKeyedServices("windows")] IOperationTransient windowsService,
        [FromKeyedServices("macOs")] IOperationTransient macOsService)
    {
        _logger.LogInformation($"Transient {_operationTransient.OperationId}");
        _logger.LogWarning($"Scoped {_operationScoped.OperationId}");
        _logger.LogError($"Singleton {_operationSingleton.OperationId}");

        _logger.LogWarning($"Windows {windowsService.OperationId}");
        _logger.LogError($"MacOs {macOsService.OperationId}");

        return Ok("Done");
    }
}
