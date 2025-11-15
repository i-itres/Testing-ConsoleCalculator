namespace Calculator;

public class Program
{
    private static Dictionary<string, Func<double, double, double>> _operations = new()
    {
        { "+", (a, b) => a + b },
        { "-", (a, b) => a - b },
        { "*", (a, b) => a * b },
        { "/", (a, b) => a / b },
        { "^", Math.Pow },
    };

    public static void Main()
    {
        PrintHeader();
        PrintInstruction();

        while (Calculate()) { }

        Print("\n\n\nДо свидания!\n", ConsoleColor.Cyan);
    }

    private static bool Calculate()
    {
        Print("Выражение: ", ConsoleColor.Blue);
        string? input = Console.ReadLine();

        if (input is ("" or null))
            return false;

        double firstNumber, secondNumber, result;
        string operation;
        if (!TryParseExpression(input, out firstNumber, out operation, out secondNumber))
            return true;

        try
        {
            result = _operations[operation](firstNumber, secondNumber);
        }
        catch
        {
            PrintError("недопустимая операция");
            return true;
        }

        Print($"Результат вычисления: {result}\n", ConsoleColor.Green);
        return true;
    }

    private static bool TryParseExpression(string input,
        out double firstNumber, out string operation, out double secondNumber)
    {
        string[] splittedInput = input.Split(' ');

        firstNumber = double.NaN;
        secondNumber = double.NaN;
        operation = null;

        try
        {
            firstNumber = double.Parse(splittedInput[0]);
            operation = splittedInput[1];
            secondNumber = double.Parse(splittedInput[2]);
        }
        catch
        {
            PrintError("некорректный ввод");
            return false;
        }

        if (splittedInput[0].Length > 16 || splittedInput[2].Length > 16)
        {
            PrintError("введённое число слишком большое");
            return false;
        }

        if (!_operations.ContainsKey(operation))
        {
            PrintError($"операция '{operation}' не содержиться в системе");
            return false;
        }

        return true;
    }

    private static void PrintError(string message)
        => Print($"Ошибка: {message}\n", ConsoleColor.Red);

    private static void PrintInstruction()
    {
        Print("ИНСТРУКЦИЯ ПО ИСПОЛЬЗОВАНИЮ ПРОГРАММЫ:\n", ConsoleColor.White, ConsoleColor.DarkYellow);
        Print("Формат ввода: 'число операция число'\n", ConsoleColor.Yellow);
        Print("Вещественные числа вводятся с отделением дробной части символом, соответсвующим региону ситемы\n", ConsoleColor.Yellow);
        Print("Допустимые операции: +, -, *, /, ^\n\n", ConsoleColor.Yellow);
        Print("Для выхода их программы введите пустую строку\n\n", ConsoleColor.Yellow);
    }

    private static void PrintHeader()
    {
        Print("КАЛЬКУЛЯТОР\n", ConsoleColor.White, ConsoleColor.Cyan);
        Print("Программа выполняет математические операции над двумя числами.\n\n", ConsoleColor.Cyan);
    }

    private static void Print(string str, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
    {
        Console.ForegroundColor = foreColor;
        Console.BackgroundColor = backColor;

        Console.Write(str);

        Console.ResetColor();
    }
}
