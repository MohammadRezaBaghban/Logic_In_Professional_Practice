using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP.Composite_Pattern.Node
{
    class TrueFalse:SingleComponent
    {

        public TrueFalse(bool data)
        {
            Data = data;
            Symbol = (data) ? '1' : '0';
            InFixFormula = (data) ? "True" : "False";
            NodeNumber = ++ParsingModule.nodeCounter;
        }

        public override void Evaluate(IVisitor visitor) => visitor.Visit(this);
    }
}
