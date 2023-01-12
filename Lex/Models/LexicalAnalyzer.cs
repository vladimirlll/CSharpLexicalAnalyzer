using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lex.Models.Exceptions.SettingExceptions;
using Lex.Models.InputSymbols;
using Lex.Models.Exceptions.AnalyzeExceptions;
using Lex.Models.States;
using Lex.Models.Tokens;

namespace Lex.Models
{
    class LexicalAnalyzer
    {
        public string Program { get; private set; }
        public List<Token> Tokens { get; private set; }
        public string ReadingLexem { get; private set; }
        private List<List<int>> transitionTable;
        public int pos { get; private set; }
        private const int START_STATE = 0;
        public int CurrentState { get; private set; }
        public int LineNum { get; private set; }

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

        private LexicalAnalyzer()
        {
            Tokens = new List<Token>();
            pos = 0;
            LineNum = 1;
            CurrentState = START_STATE;
            ReadingLexem = "";
        }

        public LexicalAnalyzer(string codeFileName, string transitionTableFileName) : this()
        {
            Program = ReadCode(codeFileName);
            transitionTable = ReadTransitionTable(transitionTableFileName);
        }

        public LexicalAnalyzer(string codeFileName, List<List<int>> tt) : this()
        {
            Program = ReadCode(codeFileName);
            transitionTable = tt;
        }

        private SymbolClass GetSymbolClass(char c)
        {
            if (c == '0') return SymbolClass.Zero;
            else if (c == '1') return SymbolClass.One;
            else if (char.IsDigit(c) && c != '0' && c != '1') return SymbolClass.TwoToNine;
            else if (c == 'x' || c == 'X') return SymbolClass.HexPrefix;
            else if (c == 'b' || c == 'B') return SymbolClass.BinPrefix;
            else if ("ABCDFabcdf".Contains(c)) return SymbolClass.LetterHexDigit;
            else if (char.IsLetter(c)) return SymbolClass.Letter;
            else if (c == '_') return SymbolClass.Underscore;
            else if (c == '+') return SymbolClass.Plus;
            else if (c == '-') return SymbolClass.Minus;
            else if (c == '/') return SymbolClass.Slash;
            else if (c == '*') return SymbolClass.Asterisk;
            else if (c == '=') return SymbolClass.Equal;
            else if (c == '<') return SymbolClass.LessThan;
            else if (c == '>') return SymbolClass.MoreThan;
            else if ("'".Contains(c)) return SymbolClass.SingleQuote;
            else if (c == '\"') return SymbolClass.DoubleQuote;
            else if (c == '&') return SymbolClass.Ampersand;
            else if (c == '%') return SymbolClass.Percent;
            else if (c == '!') return SymbolClass.Exclamation;
            else if (c == '|') return SymbolClass.VerticalLine;
            else if (c == '.') return SymbolClass.Dot;
            else if (c == '[' || c == ']' || c == '{' || c == '}' || c == '(' || c == ')' ||
                c == '.' || c == ',' || c == ':' || c == ';') return SymbolClass.Punctuator;
            else if (char.IsWhiteSpace(c)) return SymbolClass.WS;
            else if (c == '\\') return SymbolClass.Backslash;
            else return SymbolClass.Other;

        }

        private TokenType GetTokenTypeOfCurrentState()
        {
            switch (CurrentState)
            {
                case (int)PreviousFinalStates.ID:
                    return TokenType.Identifier;

                case (int)PreviousFinalStates.Div:
                case (int)FinalStates.Increment:
                case (int)FinalStates.PlusAssign:
                case (int)PreviousFinalStates.Plus:
                case (int)FinalStates.Decrement:
                case (int)FinalStates.MinusAssign:
                case (int)PreviousFinalStates.Minus:
                case (int)PreviousFinalStates.Assign:
                case (int)FinalStates.Equal:
                case (int)PreviousFinalStates.Mul:
                case (int)FinalStates.MulAssign:
                case (int)FinalStates.DivAssign:
                case (int)FinalStates.ShiftLeft:
                case (int)FinalStates.LessOrEqual:
                case (int)PreviousFinalStates.LessThan:
                case (int)PreviousFinalStates.MoreThan:
                case (int)FinalStates.ShiftRight:
                case (int)FinalStates.MoreOrEqual:
                case (int)FinalStates.BitMultAssign:
                case (int)FinalStates.LogicalMult:
                case (int)FinalStates.BitOrAssign:
                case (int)FinalStates.LogicalOr:
                case (int)FinalStates.NegativeAssign:
                case (int)PreviousFinalStates.Negative:
                case (int)PreviousFinalStates.BitMul:
                case (int)PreviousFinalStates.BitOr:
                    return TokenType.Operator;

                case (int)FinalStates.StringLiteral:
                    return TokenType.StringLiteral;
                case (int)FinalStates.CharLiteral:
                    return TokenType.CharacterLiteral;
                case (int)PreviousFinalStates.DecimalIntLiteral:
                case (int)PreviousFinalStates.BinIntLiteral:
                case (int)PreviousFinalStates.HexIntLiteral:
                case (int)PreviousFinalStates.ZeroDecimalIntLiteral:
                    return TokenType.IntegerLiteral;
                case (int)PreviousFinalStates.RealLiteral:
                    return TokenType.RealLiteral;

                case (int)FinalStates.Punctuator:
                    return TokenType.Punctuator;

                default:
                    throw new NotFinishStateException(this);
            }
        }

        public void Analyze()
        {
            ReadingLexem = "";
            pos = 0;
            int startLexemPos = 0;
            int prevState = -1;
            CurrentState = START_STATE;

            for (; pos != Program.Length; pos++)
            {
                char symbol = Program[pos];
                SymbolClass sc = GetSymbolClass(symbol);
                prevState = CurrentState;
                CurrentState = transitionTable[CurrentState][(int)sc];

                if (symbol == '\n') LineNum++;

                if (CurrentState >= 0)
                {
                    if (Enum.IsDefined(typeof(FinalStates), CurrentState))
                    {
                        if (CurrentState != (int)FinalStates.Comment
                            && !(CurrentState == (int)FinalStates.CheckPreviousState
                            && prevState == (int)PreviousFinalStates.WSEnd))
                        {
                            if (CurrentState == (int)FinalStates.CheckPreviousState)
                            {
                                CurrentState = prevState;
                                pos--;
                            }

                            int lexemLength = 0;
                            lexemLength = pos - startLexemPos + 1;

                            ReadingLexem = Program.Substring(startLexemPos, lexemLength);

                            TokenType type = GetTokenTypeOfCurrentState();
                            if (CurrentState == (int)PreviousFinalStates.ID && keywords.Contains(ReadingLexem))
                                type = TokenType.Keyword;

                            Tokens.Add(new Token(type, ReadingLexem, "Номер строки - " + LineNum));
                        }

                        if (CurrentState != (int)FinalStates.CheckPreviousState)
                            startLexemPos = pos + 1;
                        else
                        {
                            startLexemPos = pos;
                            pos--;
                        }
                        CurrentState = START_STATE;
                    }
                }
                else throw new UnavailableTransitionException(this);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var token in Tokens)
            {
                sb.Append(token + "\n\n");
            }
            return sb.ToString();
        }

    }
}
