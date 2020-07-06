using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;

namespace LPP.Composite_Pattern.Connectives
{
    public class BiImplication : CompositeComponent
    {
        public BiImplication() => Symbol = '=';
        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}