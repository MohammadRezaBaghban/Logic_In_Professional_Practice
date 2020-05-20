using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Node;

namespace LPP.NodeComponents
{
    public class PropositionalVariables
    {

        private readonly List<PropositionalVariable> propositionalVariables;

        public PropositionalVariables()
        {
            propositionalVariables = new List<PropositionalVariable>();
        }
        public void AddPropsitionalVaraiable(PropositionalVariable propositionalVariable)
        {
            if (!propositionalVariables.Contains(propositionalVariable))
            {
                propositionalVariables.Add(propositionalVariable);
            }
            else
            {
                throw new LPPException("The proposition has already was in the list");
            }
        }

        public PropositionalVariable[] GetPropositionalVariables()
        {
            var list = new PropositionalVariable[propositionalVariables.Count];
            propositionalVariables.OrderBy(x => x.Symbol).ToList().CopyTo(list);
            return list;
        }

        public PropositionalVariable GetPropositionalVariable(char symbol) =>
            propositionalVariables.Find(p => p.Symbol == symbol);
    }
}