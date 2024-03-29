﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Tokens
{
    class Token
    {
        public TokenType Type { get; }
        public string Lexem { get; }
        public string Attribute { get; }

        public Token(TokenType tokenType, string lexem, string attribute = "")
        {
            Type = tokenType;
            Lexem = lexem;
            Attribute = attribute;
        }

        public override string ToString()
        {
            return "Тип токена - " + Type + "\nЛексема - " + Lexem 
                + "\nАттрибут токена - " + Attribute;
        }
    }
}
