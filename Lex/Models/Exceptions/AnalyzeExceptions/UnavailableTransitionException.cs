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
        public string ReadingLexem { get; private set; }
        public int PosInCode { get; private set; }
        public int LineNum { get; private set; }
        public UnavailableTransitionException(LexicalAnalyzer la) : base(MSG)
        {
            State = la.CurrentState;
            Symbol = la.Program[la.pos];
            ReadingLexem = la.ReadingLexem;
            PosInCode = la.pos;
            LineNum = la.LineNum;
        }

        public override string GetMessage()
        {
            return MSG + "\nСостояние - " + State + ", входной символ - " + Symbol
                + "\nСчитанная лексема - " + ReadingLexem + ", позиция входного символа в тектсе программы" + PosInCode
               + "\nНомер строки - " + LineNum;
        }
    }
}
