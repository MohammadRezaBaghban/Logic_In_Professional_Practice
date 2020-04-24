using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP.Composite_Pattern
{
    public abstract class SingleComponent:Component
    {
        //Fields
        public bool IsPropositionalVariable;
        public override string GraphVizFormula
        {
            get => $"node{NodeNumber} [ label = \"{Symbol}\" ]";
        }

        //Methods
        public override void Evaluate(IVisitor visitor)
        {

        }

        public override string ToString() => $"Variable {Symbol} - Value: {Data}";
    }
}
