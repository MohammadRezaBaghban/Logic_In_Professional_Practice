using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Parsing_BinaryTree;

namespace LPP.Composite_Pattern
{
    public interface IVisitor
    {
        void Calculate(Component visitable);

        void Visit(BiImplication visitable);
        void Visit(Implication visitable);
        void Visit(Disjunction visitable);
        void Visit(Conjunction visitable);
        void Visit(Negation visitable);
        void Visit(Nand visitable);

    }
}