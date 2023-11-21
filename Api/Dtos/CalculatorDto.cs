namespace Api.Dtos;

public class CalculatorRequestDto
{
    public double A { get; set; }
    public double B { get; set; }
    public Operation Operation { get; set; }
}

public class CalculatorResponseDto
{
    public double Response { get; set; }
}

public enum Operation
{
    SUM = 0,
    SUBTRACTION = 1,
    DIVISION = 2,
    MULTIPLICATION = 3
}