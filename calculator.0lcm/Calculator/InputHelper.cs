using System.ComponentModel.DataAnnotations;

namespace CalculatorProgram
{
    internal class InputHelper
    {
        public static string GetStringInput(string? prompt = null, bool doToLower = true, bool doTrim = true, IReadOnlyCollection<string>? validInputs = null, string? customInvalidInputPrompt = null)
        {
            string invalidInputPrompt = customInvalidInputPrompt ?? "This is not a valid input. Try again.";
            string? userInput;

            while (true)
            {
                if (!string.IsNullOrEmpty(prompt))
                {
                    Console.WriteLine($"\n{prompt}");
                }

                userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Input cannot be empty. Try again.");
                    continue;
                }

                if (doToLower) userInput = userInput.ToLower();
                if (doTrim) userInput = userInput.Trim();

                if (validInputs != null && validInputs.Count > 0)
                {
                    bool isValid = validInputs
                        .Where(v => v != null)
                        .Any(v => string.Equals(v, userInput, StringComparison.OrdinalIgnoreCase));

                    if (!isValid)
                    {
                        Console.WriteLine(invalidInputPrompt);
                        continue;
                    }
                }
                return userInput;
            }
        }

        public static T GetEnumInput<T>(Dictionary<string, T> shortcutMap, string? menu = null, string? prompt = null) where T : Enum
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(menu))
                {
                    Console.WriteLine(menu);
                }
                if (!string.IsNullOrEmpty(prompt))
                {
                    Console.Write(prompt);
                }

                string? userInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Input cannot be empty. Try again.");
                    continue;
                }

                if(shortcutMap.TryGetValue(userInput, out T value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("This isnt a valid input. Try again.");
                    continue;
                }
            }
        }

        public static double GetDoubleInput(string? prompt = null)
        {
            string? userInput;

            while (true)
            {
                if (!string.IsNullOrEmpty(prompt))
                {
                    Console.Write($"\n{prompt}");
                }

                userInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Input cannot be empty. Try again.");
                    continue;
                }

                if (double.TryParse(userInput, out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Input must be a number. Try again.");
                    continue;
                }
            }
        }
    }
}
