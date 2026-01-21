namespace CalculatorLibrary
{
    public class CalculationHistory
    {
        public int UseCount { get; private set; } = 0;
        public List<string> PastCalculations { get; } = new();

        public bool HasCalculations => PastCalculations.Count > 0;

        public void Record(string calculationText)
        {
            UseCount++;
            PastCalculations.Add(calculationText);
        }

        public void ClearHistory()
        {
            PastCalculations.Clear();
        }

        public bool TryExtractResult(string calculationText, out double result)
        {
            int equalsIndex = calculationText.LastIndexOf('=');
            if (equalsIndex == -1 || equalsIndex +1 >= calculationText.Length)
            {
                result = double.NaN;
                return false;
            }

            string resultPart = calculationText[(equalsIndex + 1)..];
            return double.TryParse(resultPart, out  result);
        }
    }
}
