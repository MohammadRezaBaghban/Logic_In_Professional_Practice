using System.Collections.Generic;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Variables;

namespace LPP.Composite_Pattern.Connectives
{
    public class Existential: CompositeComponent,IVariableContainer
    {
        public PropositionalVariables ObjectVariables { get; set; }

        public Existential()
        {
            ObjectVariables = new PropositionalVariables();
            Symbol = '∀';
        }

        public override void Evaluate(IVisitor visitor) => visitor.Visit(this);

    }
}
