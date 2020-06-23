namespace LPP.Composite_Pattern.Components
{
    public abstract class SingleComponent:Component
    {
        //Fields
        public override string GraphVizFormula => $"node{NodeNumber} [ label = \"{Symbol}\" ]";

        //Methods
        public override string ToString() => $"Variable {Symbol} - Value: {Data} | Parent: {this.Parent.GetType().Name}";
    }
}
