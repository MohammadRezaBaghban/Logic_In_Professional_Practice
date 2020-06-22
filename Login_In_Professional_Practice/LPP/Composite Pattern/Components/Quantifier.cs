using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP.Composite_Pattern.Components
{
    public abstract class Quantifier : CompositeComponent
    {
        public char[] BoundVariables;
    }
}
