using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Tokens
{
    enum TokenType
    {
        Identifier = 2,
        Keyword,
        IntegerLiteral,
        RealLiteral,
        CharacterLiteral,
        StringLiteral,
        Operator,
        PunctuatorOrSeparator,
        Comment,
        WS
    }
}
