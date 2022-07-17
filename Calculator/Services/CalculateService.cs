using Calculator.Dtos;

namespace Calculator.Services;

public interface ICalculateService
{
    public double Calculate(CalculatorRequestDto calculatorRequest);
}

public class CalculateService : ICalculateService
{
    private readonly ILogger<CalculateService> _logger;
    private const string Context = "CalculateService";
    public CalculateService(ILogger<CalculateService> logger)
    {
        _logger = logger;
    }

    public double Calculate(CalculatorRequestDto calculatorRequest)
    {
        _logger.LogTrace("{Context}:Calculate - Starting operation...", Context);
        return calculatorRequest.Operation switch
        {
            Operation.SUM => calculatorRequest.A + calculatorRequest.B,
            Operation.SUBTRACTION => calculatorRequest.A - calculatorRequest.B,
            Operation.MULTIPLICATION => calculatorRequest.A * calculatorRequest.B,
            Operation.DIVISION => calculatorRequest.A / calculatorRequest.B,
            _ => 0.0
        };
    }
}