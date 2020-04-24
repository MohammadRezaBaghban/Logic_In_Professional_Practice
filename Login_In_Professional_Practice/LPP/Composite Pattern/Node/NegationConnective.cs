using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPP.NodeComponents;

namespace LPP.Composite_Pattern.Node
{
    public class NegationConnective : CompositeComponent
    {

        public NegationConnective() => Symbol = '¬';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}
