using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class  Disjunction : CompositeComponent
    {

        public Disjunction() => Symbol = '|';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}