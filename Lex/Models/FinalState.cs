using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models
{
    enum FinalStates
    {
        ID = 2,
        StringLiteral = 4,
        Comment = 10,

        Increment = 12,
        PlusAssign = 13,
        Plus = 14,

        Decrement = 16,
        MinusAssign = 17,
        Minus = 18,

        Assign = 20,
        Equal = 21,

        Mul = 23,
        MulAssign = 24,

        Div = 26,
        DivAssign = 27,

        ShiftLeft = 30,
        LessOrEqual = 34,
        LessThan = 29,

        MoreThan = 32,
        ShiftRight = 33,
        MoreOrEqual = 35,

        DecimalIntLiteral = 55,
        HexIntLiteral = 38,
        BinIntLiteral = 40,
        RealLiteral = 56,
        CharLiteral = 46,

        LogicalOrBitMult = 48,
        LogicalOrBitSum = 50,
        Negative = 52,

        Punctuator = 53,

        WSEnd = 57,
    }
}
