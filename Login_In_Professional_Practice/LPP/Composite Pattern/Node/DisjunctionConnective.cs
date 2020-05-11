using LPP.NodeComponents;

namespace LPP.Composite_Pattern.Node
{
    public class  DisjunctionConnective : CompositeComponent
    {

        public DisjunctionConnective() => Symbol = '|';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}