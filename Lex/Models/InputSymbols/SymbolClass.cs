using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.InputSymbols
{
    enum SymbolClass
    {
        Zero,                   // [0]
        One,                    // [1]
        TwoToNine,              // [2-9]
        HexPrefix,              // [xX]
        LetterHexDigit,         // [a-fA-F]
        BinPrefix,              // bB
        BinDigit,               // [0-1]
        Letter,                 // [a-zA-Z]
        Underscore,             // _
        Plus,                   // +
        Minus,                  // -
        Slash,                  // /
        Asterisk,               // *
        Equal,                  // =
        LessThan,               // <
        MoreThan,               // >
        SingleQuote,            // '
        DoubleQuote,            // "
        Ampersand,              // &
        Percent,                // %
        Exclamation,            // !
        VerticalLine,           // |
        Dot,                    // .
        Punctuator,             // [[]{}().,:;]
        WS,                     // Любой символ из категории пробелов
        Backslash,              // \
        Other                   // Любой другой символ
    }
}
