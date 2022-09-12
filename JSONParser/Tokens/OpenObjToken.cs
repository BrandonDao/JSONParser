using System;

namespace JSONParser.Tokens
{
    public struct OpenObjToken : IToken
    {
        public TokenTypes Type => TokenTypes.OpenObj;

        public string Value => "{";

        public static bool CanParse(string value)
        {
            return value == "{";
        }
    }
}
