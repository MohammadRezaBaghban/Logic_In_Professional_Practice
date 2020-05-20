using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP.Composite_Pattern.Node
{
    public class PropositionalVariable : SingleComponent, IEquatable<PropositionalVariable>
    {
        public PropositionalVariable(char symbol) =>
            (IsPropositionalVariable, InFixFormula, Symbol, NodeNumber) = (true, symbol.ToString(), symbol, ++ParsingModule.nodeCounter);

        public bool Equals(PropositionalVariable other) => this.Symbol.Equals(other.Symbol);


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropositionalVariable)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
