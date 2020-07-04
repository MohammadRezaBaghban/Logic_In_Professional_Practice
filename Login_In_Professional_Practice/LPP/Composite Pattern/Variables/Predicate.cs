using System.Linq;
using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;

namespace LPP.Composite_Pattern.Variables
{
    public class Predicate:SingleComponent,IVariableContainer
    {
        public PropositionalVariables ObjectVariables { get; set; }

        public Predicate(char symbol)
        {
            (InFixFormula, Symbol, NodeNumber) = (symbol.ToString(), symbol, ++ParsingModule.NodeCounter);
            ObjectVariables = new PropositionalVariables();
        }

        public string Variables()
        {
            var str = ObjectVariables?.Variables
                .SelectMany(x => x.Symbol.ToString()).Aggregate("", (current, next) => current += $"{next},");
            return str.Remove(str.Length - 1);
        }

        public void Evaluate(IVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"Predicate {Symbol} - Variables: {this.Variables()} | Parent: {this.Parent.GetType().Name}";
        public override string GraphVizFormula => $"node{NodeNumber} [ label = \"{Symbol}({Variables()})\" ]";
    }
}
