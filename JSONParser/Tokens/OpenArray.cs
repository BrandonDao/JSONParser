using System;

namespace JSONParser.Tokens
{
    public struct OpenArrayToken : IToken
    {
        public TokenTypes Type => TokenTypes.OpenArray;

        public string Value => "[";

        public static bool CanParse(string value)
        {
            return value == "[" || value == ":[";
        }
    }
}
