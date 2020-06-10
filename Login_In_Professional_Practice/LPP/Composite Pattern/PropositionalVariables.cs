using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Node;

namespace LPP.Composite_Pattern
{
    public class PropositionalVariables
    {

        public readonly List<PropositionalVariable> Variables;

        public PropositionalVariables() => Variables = new List<PropositionalVariable>();

        public void AddPropositionalVariable(PropositionalVariable propositionalVariable) => 
            Variables.Add(propositionalVariable);
        

        public void SetValue_Of_Propositional_Variables(char symbol, bool value) =>
            Variables.FindAll(x => x.Symbol == symbol).ForEach(x=>x.Data=value);


        public char[] Get_Distinct_PropositionalVariables_Chars() => 
            Get_Distinct_PropositionalVariables().OrderBy(x => x.Symbol).Select(x => x.Symbol).ToArray();
        

        public PropositionalVariable[] Get_Distinct_PropositionalVariables() =>
            Variables.Distinct().OrderBy(x => x.Symbol).ToArray();


        
    }
}