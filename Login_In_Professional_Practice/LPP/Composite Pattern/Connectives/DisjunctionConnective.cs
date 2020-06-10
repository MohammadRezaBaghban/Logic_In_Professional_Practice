using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class  DisjunctionConnective : CompositeComponent
    {

        public DisjunctionConnective() => Symbol = '|';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}