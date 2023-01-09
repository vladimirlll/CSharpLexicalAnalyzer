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
        public string currentLexem { get; private set; }
        private List<List<int>> transitionTable;
        public int pos { get; private set; }
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
                for (int j = 0; j < currentRowLength; ++j)
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
            pos = 0;
            CurrentlyRedingRowNum = 0;
            CurrentState = StartState;
            currentLexem = "";
        }

        private TokenType GetTokenTypeOfCurrentState()
        {
            if(Enum.IsDefined(typeof(FinalStates), CurrentState))
            {
                switch (CurrentState)
                {
                    case (int)FinalStates.ID:
                        return TokenType.Identifier;

                    case (int)FinalStates.Div:
                    case (int)FinalStates.Increment:
                    case (int)FinalStates.PlusAssign:
                    case (int)FinalStates.Plus:
                    case (int)FinalStates.Decrement:
                    case (int)FinalStates.MinusAssign:
                    case (int)FinalStates.Minus:
                    case (int)FinalStates.Assign:
                    case (int)FinalStates.Equal:
                    case (int)FinalStates.Mul:
                    case (int)FinalStates.MulAssign:
                    case (int)FinalStates.DivAssign:
                    case (int)FinalStates.ShiftLeft:
                    case (int)FinalStates.LessOrEqual:
                    case (int)FinalStates.LessThan:
                    case (int)FinalStates.MoreThan:
                    case (int)FinalStates.ShiftRight:
                    case (int)FinalStates.MoreOrEqual:
                    case (int)FinalStates.LogicalOrBitMult:
                    case (int)FinalStates.LogicalOrBitSum:
                    case (int)FinalStates.Negative:
                        return TokenType.OperatorOrPunctuator;

                    case (int)FinalStates.StringLiteral:
                        return TokenType.StringLiteral;
                    case (int)FinalStates.CharLiteral:
                        return TokenType.CharacterLiteral;
                    case (int)FinalStates.DecimalIntLiteral:
                    case (int)FinalStates.BinIntLiteral:
                    case (int)FinalStates.HexIntLiteral:
                        return TokenType.IntegerLiteral;
                    case (int)FinalStates.RealLiteral:
                        return TokenType.RealLiteral;

                    case (int)FinalStates.Punctuator:
                        return TokenType.OperatorOrPunctuator;

                    default:
                        throw new Exception("Текущее состояние не является заключительным");
                }
            }
            else throw new Exception("Текущее состояние не является заключительным");
        }

        public void Analyze()
        {
            currentLexem = "";
            while (pos != Program.Length)
            {
                SymbolClass SC = SymbolClassHandler.GetSymbolClass(Program[pos]);
                if (Program[pos] == '\n') CurrentlyRedingRowNum++;
                int oldState = CurrentState;
                CurrentState = transitionTable[CurrentState][(int)SC];
                if (Enum.IsDefined(typeof(FinalStates), CurrentState))
                {
                    if(CurrentState != (int)FinalStates.Comment && CurrentState != (int)FinalStates.WSEnd)
                    {
                        TokenType type = GetTokenTypeOfCurrentState();
                        if (CurrentState == (int)FinalStates.ID && keywords.Contains(currentLexem))
                            type = TokenType.Keyword;

                        Tokens.Add(new Token(type, currentLexem, "Номер строки - " + CurrentlyRedingRowNum));
                    }
                    currentLexem = "";
                    CurrentState = 0;
                }
                else if (CurrentState < 0) throw new UnavailableTransitionException(oldState, Program[pos]);
                else
                {
                    currentLexem += Program[pos];
                    pos++;
                }

            }
        }

    }
}
