using Calculator;

class Program
{
    static void Main(string[] _)
    {
        var service = new CalculatorService();
        service.Run();
        Console.WriteLine("The end.");
    }
}