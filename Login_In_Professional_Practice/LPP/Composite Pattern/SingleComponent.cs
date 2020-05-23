namespace LPP.Composite_Pattern
{
    public abstract class SingleComponent:Component
    {
        //Fields
        public bool IsPropositionalVariable;
        public override string GraphVizFormula => $"node{NodeNumber} [ label = \"{Symbol}\" ]";

        //Methods
        public override string ToString() => $"Variable {Symbol} - Value: {Data}";
    }
}
