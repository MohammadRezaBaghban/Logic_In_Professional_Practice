using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class NANDConnective: CompositeComponent
    {
        public NANDConnective() => Symbol = '%';
        public override void Evaluate(IVisitor c) => c.Visit(this);
    }
}
