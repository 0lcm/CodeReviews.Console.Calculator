namespace CalculatorLibrary;
public enum MathOperation
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Sqrt,
    Power,
    Tenfold,
}
public enum MainMenuSelection
{
    Calculate,
    ShowPreviousCalculations,
    Exit,
}
public enum PreviousCalculationScreenSelection
{
    Delete,
    Exit,
}
public static class  EnumMapping
{

    public static readonly Dictionary<string, MathOperation> MathOperationMap = new(StringComparer.OrdinalIgnoreCase)
        {
            {"Add", MathOperation.Add},
            {"a", MathOperation.Add},
            {"+", MathOperation.Add},
            {"Subtract", MathOperation.Subtract},
            {"s", MathOperation.Subtract},
            {"-", MathOperation.Subtract},
            {"Multiply", MathOperation.Multiply},
            {"m", MathOperation.Multiply},
            {"*", MathOperation.Multiply},
            {"Divide", MathOperation.Divide},
            {"d", MathOperation.Divide},
            {"/", MathOperation.Divide},
            {"sqrt", MathOperation.Sqrt},
            {"Square root", MathOperation.Sqrt},
            {"root", MathOperation.Sqrt},
            {"pow", MathOperation.Power},
            {"Power", MathOperation.Power},
            {"10x", MathOperation.Tenfold},
            {"Tenfold", MathOperation.Tenfold},
        };
    public static readonly Dictionary<string, MainMenuSelection> MenuSelection = new(StringComparer.OrdinalIgnoreCase)
    {
        {"c", MainMenuSelection.Calculate},
        {"Calculate", MainMenuSelection.Calculate},
        {"p", MainMenuSelection.ShowPreviousCalculations},
        {"Previous", MainMenuSelection.ShowPreviousCalculations},
        {"Previous calculations", MainMenuSelection.ShowPreviousCalculations},
        {"e", MainMenuSelection.Exit},
        {"Exit", MainMenuSelection.Exit},
    };
    public static readonly Dictionary<string, PreviousCalculationScreenSelection> PreviousCalculationSelection = new(StringComparer.OrdinalIgnoreCase)
    {
        {"d", PreviousCalculationScreenSelection.Delete},
        {"Delete", PreviousCalculationScreenSelection.Delete},
        {"e", PreviousCalculationScreenSelection.Exit},
        {"Exit", PreviousCalculationScreenSelection.Exit},
    };
}
