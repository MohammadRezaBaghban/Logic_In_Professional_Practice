using System;
using LPP.Composite_Pattern.Components;
using LPP.Modules;

namespace LPP.Composite_Pattern.Variables
{
    public class Variable : SingleComponent, IEquatable<Variable>
    {

        public bool bindVariable = false;

        public Variable(char symbol, bool bind = false) =>
            (InFixFormula, Symbol, NodeNumber,bindVariable) = (symbol.ToString(), symbol, ++ParsingModule.NodeCounter,bind);

        public bool Equals(Variable other) => this.Symbol.Equals(other.Symbol);


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Variable)obj);
        }

        public override int GetHashCode() => (int) Symbol;
    }
}
