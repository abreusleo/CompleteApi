using CompleteApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompleteApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RabbitMqController : ControllerBase
{
    private readonly ILogger<RabbitMqController> _logger;
    private readonly IMessageService _messageService;
    private const string Context = "RabbitMqController";
    public RabbitMqController(ILogger<RabbitMqController> logger, IMessageService messageService)
    {
        _logger = logger;
        _messageService = messageService;
    }
    
    [HttpPost(Name = "EnqueueMessage")]
    public IActionResult Enqueue([FromBody] string message)
    {
        _logger.LogInformation("{Context}:Enqueue - Route requested", Context);
        return Ok(_messageService.Enqueue(message));
    }
}