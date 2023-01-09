using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Exceptions.AnalyzeExceptions
{
    class NotFinishStateException : LexAnException
    {
        private const string MSG = "Текущее состояние не является заключительным";
        public int State { get; private set; }
        public NotFinishStateException(LexicalAnalyzer la) : base(MSG)
        {
            State = la.CurrentState;
        }

        public override string GetMessage()
        {
            return MSG + "\nТекущее состояние - " + State;
        }
    }
}
