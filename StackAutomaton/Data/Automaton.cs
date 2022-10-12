using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackAutomaton.Data.SpecialCase;

namespace StackAutomaton.Data
{
    /**
     * (i+i*i#, E#, 1) -> metszésében az van hogy (TE', 1)
     * (i+i*i#, TE'#, 1) -> FT', 4
     * (i+i*i#, FT'E'#, 14) -> i,8
     * (+i*i#, T'E'#, 148) -> pop
     * (+i*i#, T'E'#, 148) -> e, 6
     * (+i*i#, E'#, 1486) -> +TE', 2 
     * (+i*i#, +TE'#, 14862) -> pop
     * (i*i#, TE'#, 14862) ->
     * addig megy amíg nincs error VAGY
     * amíg nincs (#,#, 14862...) -> elfogad
     */

    public class STAutomaton
    {
        public Dictionary<string, string> Rules = new Dictionary<string, string>();
        public Stack<string> Stack = new Stack<string>();
        public List<AutomatonStep> Steps = new List<AutomatonStep>();

        private SpecialCaseFactory Factory;

        public string Input;
        public string Output;
        public string RuleOrder = string.Empty;
        public bool DidAccept = false;

        int i = 0;
        public STAutomaton(Dictionary<string, string> Rules, string Input)
        {
            Factory = new SpecialCaseFactory("");
            this.Rules = Rules;
            Input += '#';
            this.Input = FormatInput(Input);
            Stack.Push("#");
            Stack.Push("E");
            this.Output = FormatInput(Input);
        }

        public void Main()
        {
            while (i < Input.Length && !DidAccept)
            {
                try
                {
                    Delta(Input[i]);
                }
                catch (Exception e)
                {
                    PushStep(e.Message, true);
                    return;
                }
            }
        }

        private void PushStep(string message, bool isError)
        {
            AutomatonStep step = new AutomatonStep(message, String.Join("", Stack), RuleOrder, isError);
            Steps.Add(step);
        }

        private void Delta(char c)
        {
            string StackRead = Stack.Pop();
            string key = StackRead + c;
            if (Rules.ContainsKey(key))
            {
                string rule = Rules[key];
                AutomatonRule automatonRule = new AutomatonRule(rule);
                automatonRule.DecodeExpression();
                string[] ValuesAsArray = automatonRule.PushValues.ToArray();
                ToStack(ValuesAsArray, automatonRule.RuleNumber);
            }
            else
            {
                throw new Exception($"Unknown rule {key}");
            }
        }

        private void ToStack(string[] values, string RuleNo)
        {
            RuleOrder += RuleNo;
            bool isSpecialCase = HandleSpecialCases(String.Join("", values[0]));

            if (!isSpecialCase)
            {
                foreach (var item in values)
                {
                    Stack.Push(item);
                }
            }

            PushStep(GetInputFromIndex(), false);
        }

        private bool HandleSpecialCases(string firstRule)
        {
            Factory.Rule = firstRule;
            ISpecialCase specialCase = Factory.GetInstance();
            i += specialCase.Increment();
            DidAccept = specialCase.Accept();
            return specialCase.IsSpecial();
        }

        private string FormatInput(string Input)
        {
            return Regex.Replace(Input, "[0-9]+", "i");
        }

        private string GetInputFromIndex()
        {
            StringBuilder sb = new StringBuilder();
            for (int j = i; j < Input.Length; j++)
            {
                sb.Append(Input[j]);
            }
            return sb.ToString();
        }
    }
}
