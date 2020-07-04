namespace LPP.Composite_Pattern.Components
{
    public abstract class Component
    {
        public Component Parent;
        public Component LeftNode;
        public Component RightNode;

        public TableauxNode Belongs;

        //This needs to be be adjusted
        public int NodeNumber { get; set; } = ++ParsingModule.NodeCounter;

        public bool Data { get; set; }

        public char Symbol { get; set; }

        public string InFixFormula { get; set; }

        public virtual string GraphVizFormula { get; }

    }


}
