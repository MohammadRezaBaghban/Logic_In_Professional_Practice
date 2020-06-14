﻿using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class Negation : CompositeComponent
    {

        public Negation() => Symbol = '~';

        public override void Evaluate(IVisitor c) => c.Visit(this);

    }
}