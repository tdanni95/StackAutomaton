using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAutomaton.Data
{
    public class AutomatonStep
    {
        public string Input;
        public string StackContent;
        public string RuleOrder;
        public bool IsError;

        public AutomatonStep(string input, string stackContent, string ruleOrder, bool isError)
        {
            Input = input;
            StackContent = stackContent;
            RuleOrder = ruleOrder;
            IsError = isError;
        }


        public override string ToString()
        {
            return $"({Input}, {StackContent}, {RuleOrder})";
        }
    }
}
