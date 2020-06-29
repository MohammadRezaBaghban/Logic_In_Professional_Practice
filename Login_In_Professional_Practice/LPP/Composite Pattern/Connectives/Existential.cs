using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Variables;

namespace LPP.Composite_Pattern.Connectives
{
    public class Existential : CompositeComponent, IVariableContainer
    {
        public PropositionalVariables ObjectVariables { get; set; }

        public Existential()
        {
            ObjectVariables = new PropositionalVariables();
            Symbol = '!';
        }

        public override void Evaluate(IVisitor visitor) => visitor.Visit(this);

        public string Variables() {
            var str = ObjectVariables?.Variables
                .SelectMany(x => x.Symbol.ToString()).Aggregate("", (current, next) => current += $"{next},");
            return str.Remove(str.Length - 1);
        }

        public override string GraphVizFormula
        {
            get
            {
                string temp = "";
                temp += $"node{NodeNumber} [ label = \"{Symbol}({Variables()})\" ]";
                if (LeftNode != null)
                {
                    temp += $"\nnode{NodeNumber} -- node{LeftNode.NodeNumber}\n";
                    temp += LeftNode.GraphVizFormula;
                }

                return temp;
            }
        }
    }
}
