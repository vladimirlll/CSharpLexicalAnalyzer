using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.InputSymbols
{
    enum TTBuilderSymbolClass
    {
        Digit,          // [0-9]
        Comma,          // ,
        Dash,           // -
        MoreThan,       // >
        Equal,          // =
        Space,          // Пробел
        Asterisk,       // *
        EOL,            // \n
        Return          // \r
    }
}
