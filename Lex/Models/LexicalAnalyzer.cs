using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Lex.Models.Exceptions.SettingExceptions;
using Lex.Models.SymbolHandlers;
using Lex.Models.Exceptions.AnalyzeExceptions;

namespace Lex.Models
{
    class LexicalAnalyzer
    {
        public string Program { get; private set; }
        public List<Token> Tokens { get; private set; }
        public string CurrentlyReadingLexem { get; private set; }
        private List<List<int>> transitionTable;
        public int ReadingPosInCode { get; private set; }
        private const int StartState = 0;
        public int CurrentState { get; private set; }
        public int CurrentlyRedingRowNum { get; private set; }
        /*private List<int> finalStates = new List<int>()
        {
            2, 4, 6, 10, 
        };
        */
        private List<string> keywords = new List<string>()
        {
            "abstract", "as", "base", "bool", "break",
            "byte", "case", "catch", "char", "checked",
            "class", "const", "continue", "decimal", "default",
            "delegate", "do", "double", "else", "enum",
            "event", "explicit", "extern", "false", "finally",
            "fixed", "float", "for", "foreach", "goto",
            "if", "implicit", "in", "int", "interface",
            "internal", "is", "lock", "long", "namespace",
            "new", "null", "NULL", "object", "operator", "out",
            "override", "params", "private", "protected", "public",
            "readonly", "ref", "return", "sbyte", "sealed",
            "short", "sizeof", "stackalloc", "static", "string",
            "struct", "switch", "this", "throw", "true",
            "try", "typeof", "uint", "ulong", "unchecked",
            "unsafe", "ushort", "using", "virtual", "void",
            "volatile", "while"
        };

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
            Program = ReadCode(codeFileName);
            transitionTable = ReadTransitionTable(transitionTableFileName);
            Tokens = new List<Token>();
            ReadingPosInCode = 0;
            CurrentlyRedingRowNum = 0;
            CurrentState = StartState;
            CurrentlyReadingLexem = "";
        }

        public void Analyze()
        {
            // Анализ this.Code
            CurrentlyReadingLexem = "";
            while (ReadingPosInCode != Program.Length)
            {
                SymbolClass currentSymbolClass = SymbolClassHandler.GetSymbolClass(Program[ReadingPosInCode]);
                if (Program[ReadingPosInCode] == '\n') CurrentlyRedingRowNum++;
                int oldState = CurrentState;
                CurrentState = transitionTable[CurrentState][(int)currentSymbolClass];
                if (CurrentState == StartState)
                {
                    if (oldState == 2)
                    {

                    }
                    Tokens.Add(new Token(TokenType.Identifier, CurrentlyReadingLexem));
                }
                else if (CurrentState < 0) throw new UnavailableTransitionException(oldState, Program[ReadingPosInCode]);
                else CurrentlyReadingLexem += Program[ReadingPosInCode];

                ReadingPosInCode++;
            }
        }

    }
}
