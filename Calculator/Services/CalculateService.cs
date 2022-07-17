using Calculator.Dtos;

namespace Calculator.Services;

public interface ICalculateService
{
    public double Calculate(CalculatorRequestDto calculatorRequest);
}

public class CalculateService : ICalculateService
{
    public double Calculate(CalculatorRequestDto calculatorRequest)
    {
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