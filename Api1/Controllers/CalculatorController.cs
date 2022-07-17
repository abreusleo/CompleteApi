using CompleteApi.Dtos;
using CompleteApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompleteApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;
    private readonly IMessageService _messageService;
    private const string Context = "CalculatorController";
    public CalculatorController(ILogger<CalculatorController> logger, IMessageService messageService)
    {
        _logger = logger;
        _messageService = messageService;
    }
    
    [HttpPost(Name = "CalculateMessage")]
    public IActionResult Calculate([FromBody] CalculatorRequestDto calculatorRequest)
    {
        _logger.LogInformation("{Context}:Calculate - Route requested", Context);
        return Ok(_messageService.Enqueue(calculatorRequest));
    }
}