using System;

namespace JSONParser.Tokens
{
    public struct CloseObjToken : IToken
    {
        public TokenTypes Type => TokenTypes.CloseObj;

        public string Value => "}";

        public static bool CanParse(string value)
        {
            return value == "}";
        }
    }
}
