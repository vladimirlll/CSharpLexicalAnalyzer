using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.SymbolHandlers
{
    enum SymbolClass
    {
        Zero,
        One,
        TwoToNine,
        HexPrefix,
        LetterHexDigit,
        BinPrefix,
        BinDigit,
        Letter,
        Underscore,
        Plus,
        Minus,
        Slash,
        Asterisk,
        Equal,
        LessThan,
        MoreThan,
        SingleQuote,
        DoubleQuote,
        Ampersand,
        Percent,
        Exclamation,
        VerticalLine,
        Dot,
        Punctuator,
        WS,
        Other
    }
}
