using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models
{
    abstract class AbstractLexemHandler
    {
        public string Lexem { get; private set; }
        public AbstractLexemHandler GetInstance()
        {
            return null;
        }
        public abstract void Handle(string code, int readingPos);
    }
}
