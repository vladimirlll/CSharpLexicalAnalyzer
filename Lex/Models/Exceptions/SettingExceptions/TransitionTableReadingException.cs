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
        public new static string GetDefaultExStr()
        {
            const string EXSTRBASE = "Ошибка при чтении таблицы переходов из файла ";
            return EXSTRBASE;
        }

        public string TransitionTableFileName { get; private set; }
        public TransitionTableReadingException(string errorTransTableFileName) : base()
        {
            TransitionTableFileName = errorTransTableFileName;
        }
    }
}
