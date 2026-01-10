using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;

    private CalculationHistory _calculationHistory;
    public Calculator(CalculationHistory calculationHistory)
    {
        _calculationHistory = calculationHistory;

        StreamWriter logFile = File.CreateText("calculatorLog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, Enum op)
    {
        string loggableOperation = "?";
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        switch (op) //see if we can clean up magic strings using enum
        {
            case MathOperation.Add:
                result = num1 + num2;
                writer.WriteValue("Add");
                loggableOperation = "+";
                break;
            case MathOperation.Subtract:
                result = num1 - num2;
                writer.WriteValue("Subtract");
                loggableOperation = "-";
                break;
            case MathOperation.Multiply:
                result = num1 * num2;
                writer.WriteValue("Multiply");
                loggableOperation = "*";
                break;
            case MathOperation.Divide:
                if (num2 == 0)
                {
                    throw new DivideByZeroException();
                }
                result = num1 / num2;
                writer.WriteValue("Divide");
                loggableOperation = "/";
                break;
            case MathOperation.Sqrt:
                result = Math.Sqrt(num1);
                writer.WriteValue("Sqrt");
                loggableOperation = "sqrt";
                break;
            case MathOperation.Power:
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power");
                loggableOperation = "^";
                break;
            case MathOperation.Tenfold:
                result = num1 *= 10;
                writer.WriteValue("Tenfold");
                loggableOperation = "10x";
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        _calculationHistory.Record($"{num1} {loggableOperation} {num2} = {result}");
        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}