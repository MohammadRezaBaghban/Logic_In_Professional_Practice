using System;
using System.Collections.Generic;
using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class Universal:Quantifier
    {
        public Universal(char[] variables)
        {
            this.BoundVariables = variables;
            Symbol = '∀';
        }

        public override void Evaluate(IVisitor visitor) => visitor.Visit(this);
    }
}
