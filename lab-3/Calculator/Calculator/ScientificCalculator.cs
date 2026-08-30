namespace Calculator;

public class ScientificCalculator
{
    private readonly Calculator _calc;

    // Tylko raz i dobrze
    public ScientificCalculator(Calculator calculator)
    {
        _calc = calculator ?? throw new ArgumentNullException(nameof(calculator));
    }

    // Potęgowanie
    public double Power(double @base, double exponent) => Math.Pow(@base, exponent);

    // Pierwiastek
    public double Sqrt(double value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Pierwiastek z liczby ujemnej niedozwolony.", nameof(value));
        }

        return Math.Sqrt(value);
    }

    // Logarytm naturalny
    public double Log(double value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Logarytm z niedodatniej liczby niedozwolony.", nameof(value));
        }

        return Math.Log(value);
    }

    // Operacje na kolekcjach
    public double Sum(IEnumerable<double> numbers)
    {
        if (numbers == null)
        {
            throw new ArgumentNullException(nameof(numbers));
        }

        return numbers.Sum();
    }

    // Średnia
    public double Avg(IEnumerable<double> numbers)
    {
        if (numbers == null)
        {
            throw new ArgumentNullException(nameof(numbers));
        }

        var list = numbers as IList<double> ?? numbers.ToList();
        if (!list.Any())
        {
            throw new ArgumentException("Nie można obliczyć średniej z pustego zbioru.", nameof(numbers));
        }

        return list.Average();
    }

    public double Max(IEnumerable<double> numbers)
    {
        if (numbers == null)
        {
            throw new ArgumentNullException(nameof(numbers));
        }

        var list = numbers as IList<double> ?? numbers.ToList();
        if (!list.Any())
        {
            throw new ArgumentException("Nie można wyznaczyć max z pustego zbioru.", nameof(numbers));
        }

        return list.Max();
    }

    public double Min(IEnumerable<double> numbers)
    {
        if (numbers == null)
        {
            throw new ArgumentNullException(nameof(numbers));
        }

        var list = numbers as IList<double> ?? numbers.ToList();
        if (!list.Any())
        {
            throw new ArgumentException("Nie można wyznaczyć min z pustego zbioru.", nameof(numbers));
        }

        return list.Min();
    }
}