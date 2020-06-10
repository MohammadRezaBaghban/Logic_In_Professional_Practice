using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;

namespace LPP.Composite_Pattern
{
    public interface IVisitor
    {
        void Calculate(Component visitable);

        void Visit(Bi_ImplicationConnective visitable);
        void Visit(ImplicationConnective visitable);
        void Visit(DisjunctionConnective visitable);
        void Visit(ConjunctionConnective visitable);
        void Visit(NegationConnective visitable);
        void Visit(NANDConnective visitable);

    }
}