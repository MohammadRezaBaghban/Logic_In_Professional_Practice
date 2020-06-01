using LPP.Modules;

namespace LPP.Composite_Pattern
{
    public abstract class Component
    {
        public Component Parent;

        public Component LeftNode;

        public Component RightNode;

        //This needs to be be adjusted
        public int NodeNumber { get; set; } = ++ParsingModule.NodeCounter;

        public bool Data { get; set; }

        public char Symbol { get; set; }

        public string InFixFormula { get; set; }

        public virtual string GraphVizFormula { get; }

    }


}
