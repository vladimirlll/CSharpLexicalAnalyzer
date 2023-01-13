using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Lex.Models.InputSymbols;
using Lex.Models.Exceptions.AnalyzeExceptions;
using Lex.Models.States;

namespace Lex.Models
{
    class TTBuilderLA
    {
        public List<List<int>> TT { get; private set; }
        private string transitionsStr;
        private int statesCount;
        private int symbolClassesCount;
        private int state;
        private int currentStartState;
        private int currentEndState;
        private List<int> currentSymbolClasses;
        private List<List<int>> myTT = new List<List<int>>()
        {
            new List<int>(){1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new List<int>(){1, 7, -1, -1, 8, 2, -1, -1, -1},
            new List<int>(){-1, -1, 3, -1, -1, -1, -1, -1, -1},
            new List<int>(){-1, -1, -1, 4, -1, -1, -1, -1, -1},
            new List<int>(){-1, -1, -1, -1, -1, 5, -1, -1, -1},
            new List<int>(){1, -1, -1, -1, -1, -1, 11, -1, -1},
            new List<int>(){6, -1, 6, -1, -1, -1, -1, -1, 10},
            new List<int>(){-1, -1, -1, -1, -1, 1, -1, -1, -1},
            new List<int>(){-1, -1, -1, 9, -1, -1, -1, -1, -1},
            new List<int>(){-1, -1, -1, -1, -1, 6, -1, -1, -1},
            new List<int>(){-1, -1, -1, -1, -1, -1, -1, 0, -1},
            new List<int>(){-1, -1, -1, -1, -1, 12, -1, -1, -1},
            new List<int>(){-1, -1, -1, -1, 8, -1, -1, -1, -1}
        };

        public TTBuilderLA(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string statesAndClassesCountLine = reader.ReadLine();
                string[] statesAndClassesCounts = statesAndClassesCountLine.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                if (statesAndClassesCounts.Length != 2) throw new Exception("Не заданы количества состояний и классов символов");
                statesCount = int.Parse(statesAndClassesCounts[0]);
                symbolClassesCount = int.Parse(statesAndClassesCounts[1]);
                transitionsStr = reader.ReadToEnd();
            }
        }

        public void Analyze()
        {
            TT = new List<List<int>>();
            currentSymbolClasses = new List<int>();
            for (int i = 0; i < statesCount; i++)
            {
                TT.Add(new List<int>());
                for (int j = 0; j < symbolClassesCount; j++)
                    TT[i].Add(-1);
            }

            int pos = 0;
            state = 0;

            while (pos != transitionsStr.Length)
            {
                char symbol = transitionsStr[pos];
                TTBuilderSymbolClass sc = GetSymbolClass(symbol);
                state = myTT[state][(int)sc];

                if (state >= 0)
                {
                    if (Enum.IsDefined(typeof(BuilderFinalState), state))
                        ProcessState(pos);

                    pos++;
                }
                else throw new Exception("Переход невозможен");
            }
        }

        private TTBuilderSymbolClass GetSymbolClass(char symbol)
        {
            if (char.IsDigit(symbol)) return TTBuilderSymbolClass.Digit;
            else if (symbol == ',') return TTBuilderSymbolClass.Comma;
            else if (symbol == '-') return TTBuilderSymbolClass.Dash;
            else if (symbol == '>') return TTBuilderSymbolClass.MoreThan;
            else if (symbol == '=') return TTBuilderSymbolClass.Equal;
            else if (symbol == ' ') return TTBuilderSymbolClass.Space;
            else if (symbol == '*') return TTBuilderSymbolClass.Asterisk;
            else if (symbol == '\n') return TTBuilderSymbolClass.EOL;
            else if (symbol == '\r') return TTBuilderSymbolClass.Return;
            else throw new Exception("Класс для символа " + symbol + " неизвестен");
        }

        private void ProcessState(int currentPos)
        {
            switch (state)
            {
                case 2:
                    currentStartState = GetNum(currentPos - 1);
                    break;
                case 7:
                    currentSymbolClasses.Add(GetNum(currentPos - 1));
                    break;
                case 10:
                    currentEndState = GetNum(currentPos - 1);
                    for (int i = 0; i < currentSymbolClasses.Count; i++)
                        TT[currentStartState][currentSymbolClasses[i]] = currentEndState;
                    currentSymbolClasses.Clear();
                    currentStartState = -1;
                    currentEndState = -1;
                    break;
                case 12:
                    for (int i = 0; i < symbolClassesCount; i++) currentSymbolClasses.Add(i);
                    break;
                default:
                    break;
            }

        }

        private int GetNum(int pos)
        {
            string numStr = transitionsStr[pos].ToString();
            int numResult;
            while (int.TryParse(numStr, out numResult) && pos >= 0 && !char.IsWhiteSpace(transitionsStr[pos]))
            {
                pos--;
                if (pos >= 0)
                {
                    numStr = numStr.Insert(0, transitionsStr[pos].ToString());
                }
            }

            if (pos >= 0) numStr.Remove(0, 1);

            numResult = int.Parse(numStr.Substring(0, numStr.Length));

            return numResult;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < TT.Count; i++)
            {
                for (int j = 0; j < TT[i].Count; j++)
                {
                    sb.Append(TT[i][j] + "\t");
                }
                if(i != (TT.Count - 1))
                    sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
