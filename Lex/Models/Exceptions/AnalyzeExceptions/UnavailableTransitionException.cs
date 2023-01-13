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
        public int PrevState { get; private set; }
        public char Symbol { get; private set; }
        public string LastLexem { get; private set; }
        public int PosInCode { get; private set; }
        public int LineNum { get; private set; }
        public UnavailableTransitionException(LexicalAnalyzer la) : base(MSG)
        {
            PrevState = la.PrevState;
            Symbol = la.Program[la.pos];
            LastLexem = la.LastProcessedLexem;
            PosInCode = la.pos;
            LineNum = la.LineNum;
        }

        public override string GetMessage()
        {
            return MSG + "\nПредыдущее состояние - " + PrevState + ", входной символ - " + Symbol
                + "\nПоследняя считанная лексема - " + LastLexem + ",\nпозиция входного символа в тектсе программы" + PosInCode
               + "\nНомер строки - " + LineNum;
        }
    }
}
