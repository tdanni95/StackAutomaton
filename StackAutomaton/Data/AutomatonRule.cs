using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAutomaton.Data
{
    public class AutomatonRule
    {
        string Expression;
        public string RuleNumber = string.Empty;
        public List<string> PushValues = new List<string>();
        public AutomatonRule(string expression)
        {
            Expression = expression;
        }

        public void DecodeExpression()
        {
            //(+TE',2)
            string[] splitted = this.Expression.Split(',');
            //Speciális szabály pl.: pop
            if(splitted.Length == 1)
            {
                PushValues.Add(splitted[0]);
                return;
            }
            PushValues = GetRules(splitted[0]);
            RuleNumber = GetRuleNumber(splitted[1]);

        }
        private List<string> GetRules(string SubString)
        {

            List<string> generatedRules = new List<string>();
            for (int i = SubString.Length- 1; i > 0; i--)
            {
                if (SubString[i] == '\'') continue;
                if (i < SubString.Length - 1 && SubString[i + 1] == '\'')
                {
                    generatedRules.Add($"{SubString[i]}'");
                }
                else
                {
                    generatedRules.Add(SubString[i].ToString());
                }

            }

            return generatedRules;
        }

        private string GetRuleNumber(string Substring)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (Substring[i] != ')')
            {
                sb.Append(Substring[i]);
                i++;
            }

            return sb.ToString();
        }

    }
}
