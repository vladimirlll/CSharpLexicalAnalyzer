﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models
{
    enum TokenType
    {
        Identifier,
        Keyword,
        IntegerLiteral,
        RealLiteral,
        CharacterLiteral,
        StringLiteral,
        OperatorOrPunctuator
    }
}