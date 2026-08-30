namespace Calculator.Tests;

public class ScientificCalculatorTests
{
    private readonly ScientificCalculator _sci = new(new Calculator());

    [Theory]
    [InlineData(2, 3, 8)]
    [InlineData(4, 0.5, 2)]
    public void Power_Works(double b, double e, double expected) => Assert.Equal(expected, _sci.Power(b, e), 8);

    [Theory]
    [InlineData(4, 2)]
    [InlineData(0, 0)]
    public void Sqrt_Works(double v, double expected) => Assert.Equal(expected, _sci.Sqrt(v), 8);

    [Fact]
    public void Sqrt_Negative_Throws() => Assert.Throws<ArgumentException>(() => _sci.Sqrt(-1));

    [Theory]
    [InlineData(Math.E, 1)]
    [InlineData(Math.E * Math.E, 2)]
    public void Log_Works(double v, double expected) => Assert.Equal(expected, _sci.Log(v), 8);

    [Fact]
    public void Log_NonPositive_Throws() => Assert.Throws<ArgumentException>(() => _sci.Log(0));

    [Fact]
    public void CollectionOperations_Works()
    {
        var data = new List<double> { 1.0, 2.0, 3.0, 4.0 };
        Assert.Equal(10.0, _sci.Sum(data), 8);
        Assert.Equal(2.5, _sci.Avg(data), 8);
        Assert.Equal(4.0, _sci.Max(data), 8);
        Assert.Equal(1.0, _sci.Min(data), 8);
    }

    [Fact]
    public void Avg_Empty_Throws() => Assert.Throws<ArgumentException>(() => _sci.Avg(new List<double>()));
}