using JSONParser.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParser
{
    public enum TokenTypes
    {
        OpenObj,
        CloseObj,
        OpenArray,
        CloseArray,
        Identifier,
        IntValue,
        StringValue
    }

    public interface IToken
    {
        public TokenTypes Type { get; }

        public string Value { get; }

        public static bool TryParse(string value, out IToken token)
        {
            token = null;

            if (OpenObjToken.CanParse(value))
            {
                token = new OpenObjToken();
            }
            else if(OpenArrayToken.CanParse(value))
            {
                token = new OpenArrayToken();
            }
            else if(CloseObjToken.CanParse(value))
            {
                token = new CloseObjToken();
            }
            else if(CloseArrayToken.CanParse(value))
            {
                token = new CloseArrayToken();
            }
            else if (IntValueToken.CanParse(value))
            {
                token = new IntValueToken(value);
            }
            else if (StringValueToken.CanParse(value))
            {
                token = new StringValueToken(value);
            }
            else if (IdentifierToken.CanParse(value))
            {
                token = new IdentifierToken(value);
            }

            if (token != null) return true;
            return false;
        }
    }
}
