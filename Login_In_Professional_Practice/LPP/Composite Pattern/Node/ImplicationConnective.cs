using LPP.NodeComponents;

namespace LPP.Composite_Pattern.Node
{
    public class ImplicationConnective : CompositeComponent
    {

        public ImplicationConnective() => Symbol = '⇒';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}