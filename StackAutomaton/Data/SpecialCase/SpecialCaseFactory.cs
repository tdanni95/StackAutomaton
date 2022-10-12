using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAutomaton.Data.SpecialCase
{
    public class SpecialCaseFactory
    {
        private string rule;
        public string Rule { get => rule; set => rule = value; }
        public SpecialCaseFactory(string rule)
        {
            Rule = rule;
        }

        public ISpecialCase GetInstance()
        {
            switch (Rule.ToLower())
            {
                case "e":
                    return new EpsilonCase();
                case "accept":
                    return new AcceptCase();
                case "pop":
                    return new PopCase();
                default:
                    return new NotSpecialCase();
            }
        }
    }
}
