using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class ImplicationConnective : CompositeComponent
    {

        public ImplicationConnective() => Symbol = '>';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}