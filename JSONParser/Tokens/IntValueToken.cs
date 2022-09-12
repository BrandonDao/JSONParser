using System;
using System.Text.RegularExpressions;

namespace JSONParser.Tokens
{
    public struct IntValueToken : IToken
    {
        public TokenTypes Type => TokenTypes.IntValue;

        public string Value { get; }
        public IntValueToken(string value)
        {
            value = value.Replace(",", "");
            value = value.Replace("]", "");
            value = value.Replace("}", "");

            Value = value;
        }

        private static Regex intValueRegex = new Regex("[0-9]+(,|]|})");
        public static bool CanParse(string value)
        {
            return intValueRegex.IsMatch(value);
        }
    }
}
