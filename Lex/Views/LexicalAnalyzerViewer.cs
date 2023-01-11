using System;
using System.IO;
using Lex.Models;

namespace Lex.Views
{
    class LexicalAnalyzerViewer
    {
        private LexicalAnalyzer LA;

        public LexicalAnalyzerViewer(LexicalAnalyzer LA)
        {
            this.LA = LA;
        }

        public void ToConsole()
        {
            Console.WriteLine(LA);
        }

        public void ToFile(string fName)
        {
            using (StreamWriter writer = new StreamWriter(fName, false))
            {
                writer.Write(LA);
            }
        }
    }
}
