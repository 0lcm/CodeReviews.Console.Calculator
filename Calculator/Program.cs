using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    private static CalculationHistory calculationHistory = new();
    private static Calculator calculator = new(calculationHistory);
    static void Main(string[] args)
    {
        bool shouldExit = false;
        while (!shouldExit)
        {
            Console.Clear();
            Console.WriteLine("Please choose a selection from the following list: ");
            string menu = """
                c - Caculate,
                p - Previous Calculations,
                e - Exit,
                """;
            MainMenuSelection selection = InputHelper.GetEnumInput(shortcutMap: EnumMapping.MenuSelection, menu: menu);

            switch (selection)
            {
                case MainMenuSelection.Calculate:
                    ExecuteCalculation();
                    break;
                case MainMenuSelection.ShowPreviousCalculations:
                    ShowPreviousCalculations();
                    break;
                case MainMenuSelection.Exit:
                    shouldExit = true;
                    break;
            }
        }
        calculator.Finish();
    }
    static void ExecuteCalculation(double? optionalStartingNumber = null)
    {
        Console.Clear();

        bool endApp = false;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {

            double numInput1 = 0;
            double numInput2= 0;
            double result = 0;

            if (optionalStartingNumber != null)
            {
                numInput1 = optionalStartingNumber ?? 0;
            }
            else
            {
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = InputHelper.GetDoubleInput();
            }

            Console.WriteLine("Choose an operator from the following list:");
            string operationMenu = """
                a|+ - Add,
                s|- - Subtract,
                m|* - Multiply,
                d|/ - Divide,
                sqrt - Square root,
                pow - Take the Power,
                10x - Tenfold,
                """;

            MathOperation op = InputHelper.GetEnumInput(shortcutMap: EnumMapping.MathOperationMap, menu: operationMenu);

            if (op != MathOperation.Sqrt && op != MathOperation.Tenfold) //needs no second number
            {
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = InputHelper.GetDoubleInput(); 
            }


            
            try
            {
                result = calculator.DoOperation(numInput1, numInput2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Cannot divide by zero. Please try again with a new calculation.");
                continue;
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to return to the main menu, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n");
        }
    }

    static void ShowPreviousCalculations()
    {
        var history = calculationHistory;
        Console.WriteLine($"Calculator has been used {history.UseCount} time(s)");

        if (!history.HasCalculations)
        {
            Console.WriteLine("No previous calculations yet..\n");
            Console.WriteLine("Press enter to return to the main menu.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("\n");
        var calculations = history.PastCalculations;

        for (int i = 0; i < calculations.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {calculations[i]}");
        }

        Console.WriteLine("\n");
        Console.WriteLine($"""
            Enter a Number 1-{calculations.Count} to use that result in a new calculation,
            Enter 'Delete' in order to clear past calculations,
            Enter 'Exit' in order to exit to the main menu,
            """);

        while (true)
        {
            string? userInput = InputHelper.GetStringInput(); 

            if (int.TryParse(userInput, out int index) && index >= 1 && index <= calculations.Count)
            {
                string selectedCalculation = calculations[index - 1];
                if (history.TryExtractResult(selectedCalculation, out double reusedValue))
                {
                    ExecuteCalculation(optionalStartingNumber:  reusedValue);
                    return;
                }
            }

            if (EnumMapping.PreviousCalculationSelection.TryGetValue(userInput, out PreviousCalculationScreenSelection command))
            {
                switch (command)
                {
                    case PreviousCalculationScreenSelection.Delete:
                        history.ClearHistory();
                        Console.WriteLine("List cleared. Press enter to return to the main menu..");
                        Console.ReadLine();
                        return;
                    case PreviousCalculationScreenSelection.Exit:
                        return;
                }
            }
            Console.WriteLine("Invalid choice, please try again.");
        }
    }
}
