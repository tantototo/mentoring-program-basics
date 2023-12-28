namespace Converter;

public class IntConverter
{
    public static int ConvertStringToInt(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException(sentence);

        var isNegative = false;
        if (sentence.FirstOrDefault().Equals('-'))
        {
            isNegative = true;
            sentence = sentence.Remove(0, 1);
        }
        
        if (sentence.Any(s => !char.IsNumber(s)))
            throw new ArgumentException("String contains invalid symbols.");

        var result = sentence.Aggregate(0, (current, symbol) => 
            checked(current * 10 + (int)(char.GetNumericValue(symbol))));

        return isNegative ? result * -1 : result;
    }
}