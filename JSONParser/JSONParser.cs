using System;
using System.Collections.Generic;
using System.Reflection;

namespace JSONParser
{
    public class JSONParser
    {
        public static List<IToken> GetTokens(string value)
        {
            var tokens = new List<IToken>();
            var currTokenLength = 1;

            for (int i = 0; i < value.Length; i++)
            {
                IToken token;

                while (!IToken.TryParse(value.Substring(i, currTokenLength), out token) && i + currTokenLength < value.Length)
                {
                    currTokenLength++;
                }
                tokens.Add(token);

                if (token != null && token.Type == TokenTypes.IntValue && token.Value[^1] == ']')
                {
                    i--;
                }

                i += currTokenLength - 1;
                currTokenLength = 1;
            }

            return tokens;
        }

        public static T Parse<T>(string value)
        {
            List<IToken> tokens = GetTokens(value);

            int currIndex = 1;
            return Parse<T>(tokens, ref currIndex);
        }
        private static T Parse<T>(List<IToken> tokens, ref int currIndex)
        {
            Type objType = typeof(T);

            if (objType.GetConstructor(Array.Empty<Type>()) == null) throw new ArgumentException("Type must have an empty constructor");

            T obj = Activator.CreateInstance<T>();
            PropertyInfo[] propInfos = objType.GetProperties();

            var propInfoMap = new Dictionary<string, PropertyInfo>();
            foreach (var propInfo in propInfos)
            {
                propInfoMap.Add(propInfo.Name, propInfo);
            }
            
            while(currIndex < tokens.Count)
            {
                IToken token = tokens[currIndex];
                if (!propInfoMap.ContainsKey(token.Value))
                {
                    currIndex++;
                    continue;
                }

                var nextToken = tokens[currIndex + 1];
                if (nextToken.Type == TokenTypes.OpenObj)
                {
                    currIndex += 2;
                    propInfoMap[token.Value].SetValue(obj, Parse<T>(tokens, ref currIndex));
                    continue;
                }

                string value = nextToken.Value;
                if (nextToken.Type == TokenTypes.IntValue)
                {
                    propInfoMap[token.Value].SetValue(obj, int.Parse(value));
                }
                else
                {
                    if (propInfoMap[token.Value].PropertyType.BaseType.Name == "Array") throw new Exception("Can't parse arrays!");

                    propInfoMap[token.Value].SetValue(obj, value == "null" ? null : value);
                }
                currIndex += 2;
            }

            return obj;
        }

        public static void VisualizeJSONInConsole(string value)
        {
            List<IToken> tokens = GetTokens(value);
            string scopeIndent = "";

            for (int i = 0; i < tokens.Count; i++)
            {
                IToken token = tokens[i];
                TokenTypes type = tokens[i].Type;

                if (type == TokenTypes.CloseObj || type == TokenTypes.CloseArray)
                {
                    scopeIndent = scopeIndent.Remove(scopeIndent.Length - 2);
                }

                if (type == TokenTypes.Identifier)
                {
                    IToken valueToken = tokens[i + 1];

                    if (valueToken.Type == TokenTypes.IntValue || valueToken.Type == TokenTypes.StringValue)
                    {
                        Console.WriteLine(string.Concat(scopeIndent, $"{token.Value} : {tokens[i + 1].Value}"));
                        i++;
                        continue;
                    }
                    else // Value is object or array
                    {
                        Console.WriteLine(string.Concat(scopeIndent, $"{token.Value} :"));
                    }
                }
                else
                {
                    Console.WriteLine(string.Concat(scopeIndent, tokens[i].Value));
                }

                if (type == TokenTypes.OpenObj || type == TokenTypes.OpenArray)
                {
                    scopeIndent += "  ";
                }
            }
        }
    }
}
