using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.States
{
    /*
     * Реальные финальные состояния, после перехода в которые нужно обрабатывать
     * образованную лексему и переходить в начальное состояние.
     * FinalState.CheckPreviousState - индикатор того, что мы считали лексему, 
     * а для определения типа токена нужно просмотреть предыдущее состояние
    */
    enum FinalStates
    {
        StringLiteral = 4,
        CheckPreviousState = 6,
        DivAssign = 8,
        Comment = 10,

        Increment = 13,
        PlusAssign = 14,

        Decrement = 16,
        MinusAssign = 17,

        Equal = 19,

        MulAssign = 21,

        ShiftLeft = 24,
        LessOrEqual = 25,

        ShiftRight = 27,
        MoreOrEqual = 28,

        CharLiteral = 37,

        BitMultAssign = 39,
        LogicalMult = 40,
        BitOrAssign = 42,
        LogicalOr = 43,
        NegativeAssign = 45,

        Punctuator = 46,
    }
}
