using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.States
{
    /*
     * Состояния из которых есть переход в состояние FinalState.CheckPreviousState
     * Все эти состояния могут являться финальными, но из них возможен переход в другие
     * состояния, поэтому для однозначного определения, что состояние является финальным,
     * любое из состояний PreviousFinalStates имеет переход в FinalState.CheckPreviousState
    */
    enum PreviousFinalStates
    {
        ID = 1,
        Div = 5,
        Plus = 12,
        Minus = 15,
        Assign = 18,
        Mul = 20,
        WSEnd = 22,
        LessThan = 23,
        MoreThan = 26,
        ZeroDecimalIntLiteral = 29,
        DecimalIntLiteral = 30,
        HexIntLiteral = 31,
        BinIntLiteral = 32,
        RealLiteral = 34,
        BitMul = 38,
        BitOr = 41,
        Negative = 44,
    }
}
