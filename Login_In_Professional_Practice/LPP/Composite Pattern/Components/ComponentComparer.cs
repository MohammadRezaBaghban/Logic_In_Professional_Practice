using System.Collections.Generic;
using LPP.Composite_Pattern.Connectives;

namespace LPP.Composite_Pattern.Components
{
    public class ComponentComparer:IComparer<Component>
    {
        public int Compare(Component x, Component y)
        {
            switch (x)
            {
                case Negation _ when x.LeftNode is Negation || x.LeftNode is Disjunction ||
                                     x.LeftNode is Implication || x.LeftNode is Universal:
                {
                    if (y?.LeftNode is Negation && 
                        (y.LeftNode is Negation || y.LeftNode is Disjunction ||
                         y.LeftNode is Implication || y.LeftNode is Universal))
                        return 0;
                    return -1;
                }
                case Negation _ when x.LeftNode is Existential:
                    return 1;
                case Conjunction _:
                    return -1;
                case Universal _:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
