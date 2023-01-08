using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Exceptions
{
    abstract class LexAnException : Exception 
    {
        private const string EXSTR = "Ошибка в работе лексического анализатора";

        public static string GetDefaultExStr() { return EXSTR; }

        public LexAnException() : base(GetDefaultExStr()) { }
    }
}
