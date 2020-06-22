using System.Collections.Generic;
using LPP.Visitor_Pattern;

namespace LPP.Composite_Pattern.Components
{
    public class TableauxNode
    {
        //Fields
        public readonly List<Component> Components;
        public TableauxNode ParentNode;
        public TableauxNode LeftNode;
        public TableauxNode RightNode;
        public RuleType Rule_Type;
        public bool? LeafIsClosed;

        //Properties
        public bool? Branched { get; set; }
        public int NodeNumber { get; set; } = ++ParsingModule.NodeCounter;


        //Constructors
        public TableauxNode(Component root, TableauxNode parent = null)
        {
            Components = new List<Component>();
            this.Rule_Type = RuleType.Rule_Default;
            this.ParentNode = parent;
            this.Branched = false;
            root.Belongs = this;
            Components.Add(root);
            if (parent != null)
                parent.LeftNode ??= this;
        }
        public TableauxNode(List<Component> components, Component processed, TableauxNode parent)
        { // Used for non-branched a-rules
            this.Components = new List<Component>();
            this.Rule_Type = RuleType.RULE_ALPHA;
            this.ParentNode = parent;
            parent.Branched = false;
            parent.LeftNode = this;
            components.ForEach(x =>
            {
                var newNode = BinaryTree.CloneNode(x, BinaryTree.Object);
                newNode.Belongs = this;
                this.Components.Add(newNode);
            });
            parent.Components.ForEach(component =>
            {
                if (component != processed)
                {
                    var newNode = BinaryTree.CloneNode(component, BinaryTree.Object);
                    newNode.Belongs = this;
                    this.Components.Add(newNode);
                }
            });
        }
        public TableauxNode(Component node, Component processed, TableauxNode parent,bool branched)
        { // Used for branched B-rules
            this.Components = new List<Component>();
            this.Rule_Type = RuleType.RULE_BETA;
            node.Belongs = this;
            Components.Add(node);
            parent.Components.ForEach(component =>
            {
                if (component != processed)
                {
                    var newNode = BinaryTree.CloneNode(component, BinaryTree.Object);
                    newNode.Belongs = this;
                    this.Components.Add(newNode);
                }
            });
            parent.Branched = branched;
            this.ParentNode = parent;
            if (parent.LeftNode == null) parent.LeftNode = this;
            else if (parent.RightNode == null && branched == true) parent.RightNode = this;

        }

        public void IsClosed()
        {
            if (LeafIsClosed != true)
            {
                this.Components.ForEach(x => InfixFormulaGenerator.Calculator.Calculate(x));
                if (this.LeftNode == null && this.RightNode == null)
                {// Node has not being Processed
                    if (Components.Count > 1)
                    {
                        FindContradiction();
                        if (Branched == false && LeftNode == null)
                        {
                            this.LeafIsClosed = false;
                        }
                        else
                        {
                            if (this.LeafIsClosed != true)
                            {
                                this.Evaluate();
                            }
                        }
                    }
                    else
                    {
                        //Root of Tableaux Node
                        this.Evaluate();
                    }
                }
                else
                {
                    //Node Has already Processed and simplified
                    this.LeafIsClosed = Branched == false
                        ? LeftNode.LeafIsClosed
                        : LeftNode.LeafIsClosed == true && this.RightNode.LeafIsClosed == true;
                }
            }
        }
        private void Evaluate()
        {
            if (LeafIsClosed == null)
            {
                TableauxCalculator.Object.Visit(this);
                if (Branched == true)
                {
                    this.LeftNode.IsClosed();
                    this.RightNode.IsClosed();
                }
                else if (Branched == false && this.LeftNode != null)
                {
                    this.LeftNode.IsClosed();
                }
                IsClosed();
            }
        }
        private void FindContradiction()
        {
            for (var i = 0; i < Components.Count && this.LeafIsClosed != true; i++)
            {
                for (var j = i + 1; j < Components.Count; j++)
                {
                    var node1Formula = Components[i].InFixFormula;
                    var node2Formula = Components[j].InFixFormula;

                    if (Components[j] is SingleComponent)
                    {
                        node2Formula = Components[j] is SingleComponent ? $"¬{node2Formula}" : $"¬({node2Formula})";
                        if (node1Formula != node2Formula) continue;
                        this.LeafIsClosed = true;
                        break;
                    }
                    else if (Components[i] is SingleComponent)
                    {
                        node1Formula = Components[i] is SingleComponent ? $"¬{node1Formula}" : $"¬({node1Formula})";
                        if (node1Formula != node2Formula) continue;
                        this.LeafIsClosed = true;
                        break;
                    }
                }
            }
        }
        
        public string Label()
        {
            var label = "";
            Components.ForEach(x => { InfixFormulaGenerator.Calculator.Calculate(x); label += x.InFixFormula + ", "; });

            if (this.LeafIsClosed == true)
                label += "\n\n CLOSED";
            else if (this.LeafIsClosed == false)
                label += "\n\n OPENED";

            return label;
        }
        public string GraphVizFormula()
        {
            string temp = "";
            temp += $"node{NodeNumber} [ label = \"{this.Label()}\" shape=rectangle style=filled" +
                    " color=" + (LeafIsClosed ==true ? "red" : "black") +
                    " fillcolor=" + (Rule_Type == RuleType.RULE_ALPHA ? "yellow" :
                        Rule_Type == RuleType.RULE_BETA ? "palegreen" :
                        Rule_Type == RuleType.RULE_DELTA ? "skyblue" :
                        Rule_Type == RuleType.RULE_GAMMA ? "brown1" :
                        Rule_Type == RuleType.RULE_OMEGA ? "darkorange" :
                        Rule_Type == RuleType.Rule_Default ? "gray88" :
                        "gray88") + "]";

            if (LeftNode != null)
            {
                temp += $"\nnode{NodeNumber} -- node{LeftNode.NodeNumber}\n";
                temp += LeftNode.GraphVizFormula();
            }
            if (RightNode != null)
            {
                temp += $"\nnode{NodeNumber} -- node{RightNode.NodeNumber}\n";
                temp += RightNode.GraphVizFormula();
            }
            return temp;
        }

        
    }

    public enum RuleType { RULE_ALPHA, RULE_BETA, RULE_DELTA, RULE_GAMMA, RULE_OMEGA,Rule_Default };
}
