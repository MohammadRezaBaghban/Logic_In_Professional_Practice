using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Node;

namespace LPP.Composite_Pattern
{
    public class PropositionalVariables
    {

        private readonly List<PropositionalVariable> _propositionalVariables;

        public PropositionalVariables() => _propositionalVariables = new List<PropositionalVariable>();

        public void AddPropositionalVariable(PropositionalVariable propositionalVariable) => 
            _propositionalVariables.Add(propositionalVariable);
        

        public void SetValue_Of_Propositional_Variables(char symbol, bool value) =>
            _propositionalVariables.FindAll(x => x.Symbol == symbol).ForEach(x=>x.Data=value);


        public char[] Get_Distinct_PropositionalVariables_Chars() => 
            Get_Distinct_PropositionalVariables().OrderBy(x => x.Symbol).Select(x => x.Symbol).ToArray();
        

        public PropositionalVariable[] Get_Distinct_PropositionalVariables() =>
            _propositionalVariables.Distinct().OrderBy(x => x.Symbol).ToArray();
        
    }
}