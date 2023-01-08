using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Exceptions.SettingExceptions
{
    class CodeReadingException : LexAnException
    {
        public new static string GetDefaultExStr()
        {
            const string EXSTRBASE = "Ошибка при чтении кода из файла ";
            return EXSTRBASE;
        }

        public string CodeFileName { get; private set; }
        public CodeReadingException(string errorCodeFileName) : base()
        {
            CodeFileName = errorCodeFileName;
        }
    }
}
