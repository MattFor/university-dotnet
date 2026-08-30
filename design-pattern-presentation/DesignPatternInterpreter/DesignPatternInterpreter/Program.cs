using System;

class Program
{
    static int Interpret(string expression)
    {
        var parts = expression.Split(' ');

        int left = int.Parse(parts[0]);
        string op = parts[1];
        int right = int.Parse(parts[2]);

        if (op == "+")
            return left + right;
        else if (op == "-")
            return left - right;
        else if (op == "*")
            return left * right;
        else if (op == "/")
            return left / right;

        throw new Exception("Nieznany operator");
    }

    static void Main()
    {
        Console.WriteLine(Interpret("3 + 4"));
    }
}



