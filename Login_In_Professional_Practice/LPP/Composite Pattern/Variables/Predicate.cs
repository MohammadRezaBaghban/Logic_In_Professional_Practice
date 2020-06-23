using System.Linq;
using LPP.Composite_Pattern.Components;

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

        public string Variables() => ObjectVariables?.Variables
                .SelectMany(x => x.Symbol.ToString())
                .Aggregate("", (current, next) => current += $"{next},");

        public override string ToString() => $"Predicate {Symbol} - Variables: {this.Variables()} | Parent: {this.Parent.GetType().Name}";
    }
}
