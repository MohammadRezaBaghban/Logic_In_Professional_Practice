using LPP.NodeComponents;

namespace LPP.Composite_Pattern.Node
{
    public class ConjuctionConnective : CompositeComponent
    {

        public ConjuctionConnective() => Symbol = '&';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}