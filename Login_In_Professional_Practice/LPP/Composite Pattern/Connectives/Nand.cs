﻿using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;

namespace LPP.Composite_Pattern.Connectives
{
    public class Nand: CompositeComponent
    {
        public Nand() => Symbol = '%';
        public override void Evaluate(IVisitor c) => c.Visit(this);
    }
}
