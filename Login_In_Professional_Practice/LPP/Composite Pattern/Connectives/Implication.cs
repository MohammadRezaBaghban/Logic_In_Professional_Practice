﻿using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class Implication : CompositeComponent
    {

        public Implication() => Symbol = '>';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}