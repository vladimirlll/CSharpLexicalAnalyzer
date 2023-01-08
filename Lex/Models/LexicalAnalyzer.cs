using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Lex.Models.Exceptions.SettingExceptions;

namespace Lex.Models
{
    class LexicalAnalyzer
    {
        public string Code { get; private set; }
        public List<Token> Tokens { get; private set; }
        private List<List<int>> transitionTable;
        //public HashSet<Token> IDTable { get; private set; }

        private List<List<int>> ReadTransitionTable(string fileName)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(fileName);
            }
            catch (Exception)
            {
                throw new TransitionTableReadingException(fileName);
            }
            int n = lines.Length;
            List<List<int>> tt = new List<List<int>>(n);
            int previousRowLength = 0;
            for (int i = 0; i < n; ++i)
            {
                string[] rowNumbers = lines[i].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                int currentRowLength = rowNumbers.Length;
                if (previousRowLength != 0 && currentRowLength != previousRowLength)
                    throw new TransitionTableReadingException(fileName);
                tt.Add(new List<int>(currentRowLength));
                for (int j = 0; j < n; ++j)
                {
                    int el = int.Parse(rowNumbers[j]);
                    tt[i].Add(el);
                }
                previousRowLength = currentRowLength;
            }

            return tt;
        }

        private string ReadCode(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                try
                {
                    return reader.ReadToEnd();
                }
                catch (Exception)
                {
                    throw new CodeReadingException(fileName);
                }
            }
        }

        public LexicalAnalyzer(string codeFileName, string transitionTableFileName)
        {
            Code = ReadCode(codeFileName);
            transitionTable = ReadTransitionTable(transitionTableFileName);
            Tokens = new List<Token>();
        }

        public void Analyze()
        {
            // Анализ this.Code

        }

    }
}
