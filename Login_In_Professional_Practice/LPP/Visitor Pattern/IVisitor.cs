﻿using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;

namespace LPP.Visitor_Pattern
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
        void Visit(Universal visitable);
        void Visit(Existential visitable);
        void Visit(Predicate visitable);
    }
}