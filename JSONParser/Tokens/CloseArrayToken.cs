using System;

namespace JSONParser.Tokens
{
    public struct CloseArrayToken : IToken
    {
        public TokenTypes Type => TokenTypes.CloseArray;

        public string Value => "]";

        public static bool CanParse(string value)
        {
            return value == "]";
        }
    }
}
