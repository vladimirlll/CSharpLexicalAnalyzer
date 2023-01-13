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
            TTBuilderLA builder = new TTBuilderLA("transitions.txt");
            builder.Analyze();
            TTBuilderViewer viewer = new TTBuilderViewer(builder, "tt.txt");
            viewer.View();

            try
            {
                LexicalAnalyzer LA = new LexicalAnalyzer("test.cs", "tt.txt");
                LA.Analyzing();
                LexicalAnalyzerViewer LAViewer = new LexicalAnalyzerViewer(LA);
                LAViewer.View();
            }
            catch (LexAnException laEx)
            {
                Console.WriteLine(laEx.GetMessage());
                Environment.Exit(1);
            }

        }
    }
}
