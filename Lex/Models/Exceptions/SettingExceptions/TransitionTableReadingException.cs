using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lex.Models.Exceptions;

namespace Lex.Models.Exceptions.SettingExceptions
{
    class TransitionTableReadingException : LexAnException
    {
        private const string MSG = "Ошибка при чтении таблицы переходов из файла";

        public string TransitionTableFileName { get; private set; }
        public TransitionTableReadingException(string fileName) : base(MSG)
        {
            TransitionTableFileName = fileName;
        }

        public override string GetMessage()
        {
            return MSG + " - " + TransitionTableFileName;
        }
    }
}
