namespace LPP.Composite_Pattern.Components
{
    public abstract class CompositeComponent : Component
    {
        public Component Nand;

        public override string GraphVizFormula
        {
            get
            {
                string temp = "";
                temp += $"node{NodeNumber} [ label = \"{Symbol}\" ]";
                if (LeftNode != null)
                {
                    temp += $"\nnode{NodeNumber} -- node{LeftNode.NodeNumber}\n";
                    temp += LeftNode.GraphVizFormula;
                }
                if (RightNode != null)
                {
                    temp += $"\nnode{NodeNumber} -- node{RightNode.NodeNumber}\n";
                    temp += RightNode.GraphVizFormula;
                }
                return temp;
            }
        }

        public abstract void Evaluate(IVisitor visitor);

        public override string ToString() =>
            $"{this.GetType().Name}"
            + $" | Data: {this.Data.ToString()}"
            + $" | Parent: {(this.Parent?.GetType().Name) ?? "Null"}"
            + $" | L: {LeftNode.GetType().Name}"
            + $" | R: {RightNode.GetType().Name}";

    }
}
