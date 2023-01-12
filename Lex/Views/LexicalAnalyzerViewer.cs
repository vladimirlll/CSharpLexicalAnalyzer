using System;
using System.IO;
using Lex.Models;

namespace Lex.Views
{
    class LexicalAnalyzerViewer : AbstractViewer
    {
        private LexicalAnalyzer LA;

        public LexicalAnalyzerViewer(LexicalAnalyzer LA)
        {
            this.LA = LA;
        }

        public override void View()
        {
            Console.WriteLine(LA);
        }
    }
}
