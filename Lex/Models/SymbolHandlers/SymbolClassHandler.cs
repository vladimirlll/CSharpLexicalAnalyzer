using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.SymbolHandlers
{
    static class SymbolClassHandler
    {
        public static SymbolClass GetSymbolClass(char c)
        {
            if (c == '0') return SymbolClass.Zero;
            else if (c == '1') return SymbolClass.One;
            else if (char.IsDigit(c) && c != '0' && c != '1') return SymbolClass.TwoToNine;
            else if (c == 'x' || c == 'X') return SymbolClass.HexPrefix;
            else if ("ABCDFabcdf".Contains(c)) return SymbolClass.LetterHexDigit;
            else if (c == 'b' || c == 'B') return SymbolClass.BinPrefix;
            else if (char.IsLetter(c)) return SymbolClass.Letter;
            else if (c == '_') return SymbolClass.Underscore;
            else if (c == '+') return SymbolClass.Plus;
            else if (c == '-') return SymbolClass.Minus;
            else if (c == '/') return SymbolClass.Slash;
            else if (c == '*') return SymbolClass.Asterisk;
            else if (c == '=') return SymbolClass.Equal;
            else if (c == '<') return SymbolClass.LessThan;
            else if (c == '>') return SymbolClass.MoreThan;
            else if (c == '\'') return SymbolClass.SingleQuote;
            else if (c == '\"') return SymbolClass.DoubleQuote;
            else if (c == '&') return SymbolClass.Ampersand;
            else if (c == '%') return SymbolClass.Percent;
            else if (c == '!') return SymbolClass.Exclamation;
            else if (c == '|') return SymbolClass.VerticalLine;
            else if (c == '[' || c == ']' || c == '{' || c == '}' || c == '(' || c == ')' ||
                c == '.' || c == ',' || c == ':' || c == ';') return SymbolClass.Punctuator;
            else if (char.IsWhiteSpace(c)) return SymbolClass.WS;
            else return SymbolClass.Other;

        }
    }
}
