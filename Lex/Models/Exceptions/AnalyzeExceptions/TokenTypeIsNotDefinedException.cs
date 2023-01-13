using Lex.Models.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Exceptions.AnalyzeExceptions
{
    class TokenTypeIsNotDefinedException : LexAnException
    {
        private const string MSG = "Тип токена, соответствующий введенной лексеме, не определен.";
        public int UnknownTokenType { get; private set; }
        public char Symbol { get; private set; }
        public string LastLexem { get; private set; }
        public int LineNum { get; private set; }
        public TokenTypeIsNotDefinedException(LexicalAnalyzer la) : base(MSG)
        {
            UnknownTokenType = la.CurrentState;
            Symbol = la.Program[la.pos];
            LastLexem = la.LastProcessedLexem;
            LineNum = la.LineNum;
        }

        public override string GetMessage()
        {
            return MSG + "\nНомер неопределенного типа токена - " + UnknownTokenType
                + "\nвходной символ - " + Symbol + "\nПоследняя считанная лексема - " + LastLexem 
                + "\nНомер строки - " + LineNum;
        }
    }
}
