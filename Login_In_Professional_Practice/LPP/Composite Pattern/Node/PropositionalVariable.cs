using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP.Composite_Pattern.Node
{
    class PropositionalVariable:SingleComponent
    {
        public PropositionalVariable(char symbol) =>
            (IsPropositionalVariable, InFixFormula, Symbol, NodeNumber) = (true, symbol.ToString(), symbol, ++ParsingModule.nodeCounter);

        public override void Evaluate(IVisitor visitor) => visitor.Visit(this);
    }
}
