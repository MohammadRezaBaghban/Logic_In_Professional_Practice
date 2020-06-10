using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class Bi_ImplicationConnective : CompositeComponent
    {

        public Bi_ImplicationConnective() => Symbol = '=';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}