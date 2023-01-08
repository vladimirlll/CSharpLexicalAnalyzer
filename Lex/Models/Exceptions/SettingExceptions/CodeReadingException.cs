using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Exceptions.SettingExceptions
{
    class CodeReadingException : LexAnException
    {
        private const string MSG = "Ошибка при чтении кода из файла";
        public string CodeFileName { get; private set; }
        public CodeReadingException(string fileName) : base(MSG)
        {
            CodeFileName = fileName;
        }

        public override string GetMessage()
        {
            return MSG + " - " + CodeFileName;
        }
    }
}
