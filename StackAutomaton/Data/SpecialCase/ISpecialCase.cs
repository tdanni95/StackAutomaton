using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAutomaton.Data.SpecialCase
{
    public interface ISpecialCase
    {
        bool Accept();
        int Increment();
        bool IsSpecial();
    }
}
