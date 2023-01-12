using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lex.Models.States
{
    enum BuilderFinalState
    {
        StartStateIsSet = 2,
        SymbolClassIsSet = 7,
        EndStateIsSet = 10,
        AllSymbolClassesAreSet = 12,
    }
}
