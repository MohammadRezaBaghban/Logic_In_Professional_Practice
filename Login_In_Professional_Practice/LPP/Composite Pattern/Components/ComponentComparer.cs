using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPP.Composite_Pattern.Connectives;

namespace LPP.Composite_Pattern.Components
{
    public class ComponentComparer:IComparer<Component>
    {
        public int Compare(Component x, Component y)
        {
            if (x is Negation)
            {
                if (x.LeftNode is Negation || x.LeftNode is Disjunction ||
                    x.LeftNode is Implication || x.LeftNode is Universal)
                    if (y.LeftNode is Negation && 
                        (y.LeftNode is Negation || y.LeftNode is Disjunction ||
                            y.LeftNode is Implication || y.LeftNode is Universal))
                        return 0;
                    else
                    {
                        return -1;
                    }

                if(x.LeftNode is Existential)
                {
                    return 1;
                }
            }
            else
            {
                if (x is Conjunction)
                    return -1;
                else if (x is Universal)
                    return 1;
            }
            return 0;
        }
    }
}
