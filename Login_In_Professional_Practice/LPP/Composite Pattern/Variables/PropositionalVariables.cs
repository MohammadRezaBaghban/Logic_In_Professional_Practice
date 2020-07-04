using System.Collections.Generic;
using System.Linq;

namespace LPP.Composite_Pattern.Variables
{
    public class PropositionalVariables
    {
        public readonly List<Variable> Variables;

        public PropositionalVariables() => Variables = new List<Variable>();

        public void AddPropositionalVariable(Variable variable) => 
            Variables.Add(variable);
        
        public void SetValue_Of_Propositional_Variables(char symbol, bool value) =>
            Variables.FindAll(x => x.Symbol == symbol).ForEach(x=>x.Data=value);


        public char[] Get_Distinct_PropositionalVariables_Chars() => 
            Get_Distinct_PropositionalVariables().OrderBy(x => x.Symbol).Select(x => x.Symbol).ToArray();
        

        public Variable[] Get_Distinct_PropositionalVariables() =>
            Variables.Distinct().OrderBy(x => x.Symbol).ToArray();

        public List<Variable> Get_BindVariables() =>
            Variables.Where(x => x.BindVariable).ToList();
    }
}