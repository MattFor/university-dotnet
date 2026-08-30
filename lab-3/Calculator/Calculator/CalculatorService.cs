using System.Globalization;

namespace Calculator;

public class CalculatorService
{
    private readonly Calculator _calc;
    private readonly ScientificCalculator _sci;

    public CalculatorService()
    {
        _calc = new Calculator();
        _sci = new ScientificCalculator(_calc);
    }

    public void Run()
    {
        Console.WriteLine("Kalkulator naukowy");
        Console.WriteLine("Dostępne operacje: +, -, *, /, ^, sqrt, log, sum, avg, max, min, exit");
        while (true)
        {
            Console.Write("> ");
            var op = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(op))
            {
                continue;
            }

            if (op == "exit")
            {
                break;
            }

            try
            {
                switch (op)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    {
                        HandleBinary(op);
                        break;
                    }
                    case "^":
                    {
                        HandlePower();
                        break;
                    }
                    case "sqrt":
                    {
                        HandleSqrt();
                        break;
                    }
                    case "log":
                    {
                        HandleLog();
                        break;
                    }
                    case "sum":
                    case "avg":
                    case "max":
                    case "min":
                    {
                        HandleCollection(op);
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Nieznana operacja.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
    }

    private void HandleBinary(string op)
    {
        var a = ReadDouble("Podaj pierwszą liczbę:");
        var b = ReadDouble("Podaj drugą liczbę:");

        double result = op switch
        {
            "+" => _calc.Add(a, b),
            "-" => _calc.Subtract(a, b),
            "*" => _calc.Multiply(a, b),
            "/" => _calc.Divide(a, b),
            _ => throw new InvalidOperationException("Nieobsługiwana operacja")
        };

        Console.WriteLine($"Wynik: {result}");
    }

    private void HandlePower()
    {
        var b = ReadDouble("Podaj podstawę:");
        var e = ReadDouble("Podaj wykładnik:");
        var r = _sci.Power(b, e);
        Console.WriteLine($"Wynik: {r}");
    }

    private void HandleSqrt()
    {
        var v = ReadDouble("Podaj liczbę >=0:");
        var r = _sci.Sqrt(v);
        Console.WriteLine($"Wynik: {r}");
    }

    private void HandleLog()
    {
        var v = ReadDouble("Podaj liczbę >0:");
        var r = _sci.Log(v);
        Console.WriteLine($"Wynik: {r}");
    }

    private void HandleCollection(string op)
    {
        Console.WriteLine("Podaj liczby oddzielone spacją:");
        var line = Console.ReadLine();
        var nums = ParseDoublesFromLine(line);

        double r = op switch
        {
            "sum" => _sci.Sum(nums),
            "avg" => _sci.Avg(nums),
            "max" => _sci.Max(nums),
            "min" => _sci.Min(nums),
            _ => throw new InvalidOperationException("Nieobsługiwana operacja")
        };

        Console.WriteLine($"Wynik: {r}");
    }

    private static IEnumerable<double> ParseDoublesFromLine(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            return Enumerable.Empty<double>();
        }

        var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        var list = new List<double>();

        foreach (var p in parts)
        {
            if (double.TryParse(p, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var d) || double.TryParse(p, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out d))
            {
                list.Add(d);
            }
            else
            {
                throw new FormatException($"Niepoprawny format liczby: '{p}'");
            }
        }

        return list;
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var s = Console.ReadLine();

            if (s == null)
            {
                throw new OperationCanceledException("Error wejścia.");
            }

            if (double.TryParse(s.Trim(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var d) || double.TryParse(s.Trim(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out d))
            {
                return d;
            }

            Console.WriteLine("Niepoprawne dane. Spróbuj jeszcze raz.");
        }
    }
}