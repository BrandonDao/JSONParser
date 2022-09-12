using System;
using System.Text.RegularExpressions;

namespace JSONParser.Tokens
{
    public struct IdentifierToken : IToken
    {
        public TokenTypes Type => TokenTypes.Identifier;

        public string Value { get; }

        public IdentifierToken(string value)
        {
            value = value.Replace("\"", "");
            value = value.Replace(":", "");

            Value = value;
        }

        private static Regex identifierRegex = new Regex("\".+\":");
        public static bool CanParse(string value)
        {
            return identifierRegex.IsMatch(value);
        }
    }
}
