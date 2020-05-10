using LPP.Composite_Pattern.Node;

namespace LPP.Composite_Pattern
{
    public interface IVisitor
    {
        void Calculate(Component visitable);

        void Visit(NegationConnective visitable);

        void Visit(ImplicationConnective visitable);

        void Visit(Bi_ImplicationConnective visitable);

        void Visit(ConjuctionConnective visitable);

        void Visit(DisjunctionConnective visitable);
    }
}