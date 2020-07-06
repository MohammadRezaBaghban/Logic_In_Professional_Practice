using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;

namespace LPP.Composite_Pattern.Connectives
{
    public class Negation : CompositeComponent
    {
        public Negation() => Symbol = '~';
        public bool GammaProcessed = false;
        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}
