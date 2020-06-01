namespace LPP.Composite_Pattern.Node
{
    public class NegationConnective : CompositeComponent
    {

        public NegationConnective() => Symbol = '~';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}
