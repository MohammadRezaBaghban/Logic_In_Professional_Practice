namespace LPP.Composite_Pattern.Node
{
    public class ConjunctionConnective : CompositeComponent
    {

        public ConjunctionConnective() => Symbol = '&';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}