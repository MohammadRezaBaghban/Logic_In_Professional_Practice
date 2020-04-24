using LPP.Composite_Pattern.Node;

namespace LPP.Composite_Pattern
{
    public interface IVisitor
    {
        void Visit(NegationConnective negationConnective);
        void Visit(ImplicationConnective negationConnective);
        void Visit(Bi_ImplicationConnective negationConnective);
        void Visit(ConjuctionConnective negationConnective);
        void Visit(DisjunctionConnective negationConnective);
    }
}