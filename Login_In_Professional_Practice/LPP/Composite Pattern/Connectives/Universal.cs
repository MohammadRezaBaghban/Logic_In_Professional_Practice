using System;
using System.Collections.Generic;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Variables;

namespace LPP.Composite_Pattern.Connectives
{
    public class Universal:CompositeComponent,IVariableContainer
    {
        public PropositionalVariables ObjectVariables { get; set; }

        public Universal()
        {
            ObjectVariables = new PropositionalVariables();
            Symbol = '∀';
        }

        public override void Evaluate(IVisitor visitor) => visitor.Visit(this);
    }
}
