using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lex.Models;
using Lex.Models.Exceptions;
using Lex.Models.Exceptions.AnalyzeExceptions;
using Lex.Models.Exceptions.SettingExceptions;

namespace Lex
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LexicalAnalyzer LA = new LexicalAnalyzer("test.cs", "tt.txt");
                LA.Analyze();
                OutTokens(LA);
            }
            catch (LexAnException laEx)
            {
                Console.WriteLine(laEx.GetMessage());
                Environment.Exit(1);
            }
        }

        static void OutTokens(LexicalAnalyzer LA)
        {
            foreach (var token in LA.Tokens)
            {
                Console.WriteLine(token.Lexem);
            }
        }
    }
}
