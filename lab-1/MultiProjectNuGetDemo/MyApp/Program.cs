namespace MultiProjectNuGetDemo;

using MyLibrary;
using MyServices;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        // 1.
        var calc = new Calculator();
        Console.WriteLine(calc.Add(5, 3));
        Console.WriteLine(calc.Subtract(5, 3));

        // 2.
        var sum = calc.Add(5, 3);
        var result = new { Operation = "Add", A = 5, B = 3, Result = sum };
        var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine(jsonResult);

        // 3.
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ILoggerService, ConsoleLogger>()
            .BuildServiceProvider();

        var logger = serviceProvider.GetService<ILoggerService>();
        logger.Log("Aplikacja uruchomiona.");

        sum = calc.Add(10, 15);
        logger.Log($"Wynik dodawania: {sum}");
    }
}