namespace Calculator.Tests;

// Tym razem Xunit.v3, poszerzamy horyzonty :)
public class CalculatorTests
{
    private readonly Calculator _calc = new();

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-1, 1, 0)]
    public void Add_Works(double a, double b, double expected) => Assert.Equal(expected, _calc.Add(a, b), 8);

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(-1, -1, 0)]
    public void Subtract_Works(double a, double b, double expected) => Assert.Equal(expected, _calc.Subtract(a, b), 8);

    [Theory]
    [InlineData(3, 4, 12)]
    [InlineData(-2, 3, -6)]
    public void Multiply_Works(double a, double b, double expected) => Assert.Equal(expected, _calc.Multiply(a, b), 8);

    [Theory]
    [InlineData(10, 2, 5)]
    public void Divide_Works(double a, double b, double expected) => Assert.Equal(expected, _calc.Divide(a, b), 8);

    [Fact]
    public void Divide_ByZero_Throws()
    {
        Assert.Throws<ArgumentException>(() => _calc.Divide(1, 0));
    }
}