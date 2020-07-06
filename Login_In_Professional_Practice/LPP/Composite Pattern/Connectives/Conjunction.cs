using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;

namespace LPP.Composite_Pattern.Connectives
{
    public class Conjunction : CompositeComponent
    {
        public Conjunction() => Symbol = '&';
        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}