using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Exceptions.AnalyzeExceptions
{
    class UnavailableTransitionException : LexAnException
    {
        private const string MSG = "Переход при данном состоянии и данном входном символе невозможен";
        public int State { get; private set; }
        public char Symbol { get; private set; }
        public UnavailableTransitionException(int state, char symbol) : base(MSG)
        {
            State = state;
            Symbol = symbol;
        }

        public override string GetMessage()
        {
            return MSG + "\nСостояние - " + State + ", входной символ - " + Symbol;
        }
    }
}
