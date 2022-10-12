using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAutomaton.Data.SpecialCase
{
    public class PopCase : ISpecialCase
    {
        public bool Accept()
        {
            return false;
        }

        public int Increment()
        {
            return 1;
        }

        public bool IsSpecial()
        {
            return true;
        }
    }
}
