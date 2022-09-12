using System;
using System.Text.RegularExpressions;

namespace JSONParser.Tokens
{
    public struct StringValueToken : IToken
    {
        public TokenTypes Type => TokenTypes.StringValue;

        public string Value { get; }

        public StringValueToken(string value)
        {
            value = value.Replace(",", "");
            value = value.Replace("]", "");
            value = value.Replace("}", "");

            Value = value;
        }

        private static Regex stringValueRegex = new Regex("\\D+(,|]|})");
        public static bool CanParse(string value)
        {
            return stringValueRegex.IsMatch(value);
        }
    }
}
