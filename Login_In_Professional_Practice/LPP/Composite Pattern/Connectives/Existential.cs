using System.Collections.Generic;
using LPP.Composite_Pattern.Components;

namespace LPP.Composite_Pattern.Connectives
{
    public class Existential: Quantifier
    {

        public List<string> boundVariables;

        public Existential(char[] variables)
        {
            this.BoundVariables = variables;
            Symbol = '∀';
        }

        public override void Evaluate(IVisitor visitor) => visitor.Visit(this);

    }
}
