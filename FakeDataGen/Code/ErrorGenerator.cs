using System.Security.Cryptography;

namespace FakeDataGen.Code
{
    public static class ErrorGenerator
    {
        private static readonly IReadOnlyDictionary<Locale, string> _alphabet = new Dictionary<Locale, string>()
        {
            { Locale.EN, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890" },
            { Locale.DE, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZäöüÄÖÜß1234567890" },
            { Locale.ES, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZñÑ1234567890" }
        }.AsReadOnly();

        public static string ApplyErrorsWithProbability(string input, double errorProbability, int maxErrors, Locale locale)
        {
            if (errorProbability == 0) return input;

            if (Random.Shared.NextDouble() <= (errorProbability * 0.01))
            {
                int errorCount = (int)Math.Ceiling(errorProbability * maxErrors);
                return ApplyError(input, errorCount, locale);
            }
            return input;
        }

        private static string ApplyError(string input, int errorCount, Locale locale)
        {
            for (int i = 0; i < errorCount; i++)
            {
                int errorType = RandomNumberGenerator.GetInt32(0, 3);
                input = errorType switch
                {
                    0 => RemoveRandomCharacter(input),
                    1 => AddRandomCharacter(input, locale),
                    2 => SwapAdjacentCharacters(input),
                    _ => input
                };
            }
            return input;
        }

        private static string RemoveRandomCharacter(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            int index = RandomNumberGenerator.GetInt32(0, input.Length);
            return input.Remove(index, 1);
        }

        private static string AddRandomCharacter(string input, Locale locale)
        {
            int index = RandomNumberGenerator.GetInt32(0, input.Length + 1);
            char randomChar = _alphabet[locale][RandomNumberGenerator.GetInt32(_alphabet[locale].Length)];

            return input.Insert(index, randomChar.ToString());
        }

        private static string SwapAdjacentCharacters(string input)
        {
            if (input.Length < 2) return input;

            int index = RandomNumberGenerator.GetInt32(0, input.Length - 1);

            char[] chars = input.ToCharArray();
            (chars[index], chars[index + 1]) = (chars[index + 1], chars[index]);

            return new string(chars);
        }
    }
}
