using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAutomaton.Data.SpecialCase
{
    public class NotSpecialCase : ISpecialCase
    {
        public bool Accept()
        {
            return false;
        }

        public int Increment()
        {
            return 0;
        }

        public bool IsSpecial()
        {
            return false;
        }
    }
}
