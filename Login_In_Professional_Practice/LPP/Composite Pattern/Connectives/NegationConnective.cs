using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class NegationConnective : CompositeComponent
    {

        public NegationConnective() => Symbol = '~';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}
