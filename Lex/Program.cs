using System;
using Lex.Models;
using Lex.Models.Exceptions;
using Lex.Views;

namespace Lex
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LexicalAnalyzer LA = new LexicalAnalyzer("test.cs", "tt.txt");
                LA.Analyzing();
                LexicalAnalyzerViewer LAViewer = new LexicalAnalyzerViewer(LA);
                LAViewer.ToConsole();
                LAViewer.ToFile("tokens.txt");
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
