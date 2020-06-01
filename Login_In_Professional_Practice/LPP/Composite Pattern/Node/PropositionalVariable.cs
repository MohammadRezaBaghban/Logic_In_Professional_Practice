using System;
using LPP.Modules;

namespace LPP.Composite_Pattern.Node
{
    public class PropositionalVariable : SingleComponent, IEquatable<PropositionalVariable>
    {
        public PropositionalVariable(char symbol) =>
            (IsPropositionalVariable, InFixFormula, Symbol, NodeNumber) = (true, symbol.ToString(), symbol, ++ParsingModule.NodeCounter);

        public bool Equals(PropositionalVariable other) => this.Symbol.Equals(other.Symbol);


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropositionalVariable)obj);
        }

        public override int GetHashCode() => (int) Symbol;
    }
}
