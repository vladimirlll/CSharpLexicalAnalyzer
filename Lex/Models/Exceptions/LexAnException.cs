using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.Exceptions
{
    abstract class LexAnException : Exception 
    {
        public LexAnException(string msg) : base(msg) { }

        public abstract string GetMessage();
    }
}
