using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class ConjunctionConnective : CompositeComponent
    {

        public ConjunctionConnective() => Symbol = '&';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}