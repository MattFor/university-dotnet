using System;
using System.Collections.Generic;

class Context
{
    public Stack<int> Stack { get; } = new Stack<int>();
}

interface IExpression
{
    void Interpret(Context context);
}

class NumberExpression : IExpression
{
    private readonly int number;

    public NumberExpression(int number)
    {
        this.number = number;
    }

    public void Interpret(Context context)
    {
        context.Stack.Push(number);
    }
}

class AddExpression : IExpression
{
    public void Interpret(Context context)
    {
        int right = context.Stack.Pop();
        int left = context.Stack.Pop();
        context.Stack.Push(left + right);
    }
}

class Program
{
    static void Main()
    {
        string input = "3 4 +";
        var context = new Context();
        var expressions = new List<IExpression>();

        foreach (var token in input.Split(' '))
        {
            if (token == "+")
                expressions.Add(new AddExpression());
            else
                expressions.Add(new NumberExpression(int.Parse(token)));
        }

        foreach (var expression in expressions)
            expression.Interpret(context);

        Console.WriteLine(context.Stack.Pop());
    }
}

