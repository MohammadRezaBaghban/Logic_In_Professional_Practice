using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;

namespace LPP.Parsing_BinaryTree
{
    public class TableauxNode
    {
        //Fields
        public List<Component> Components;
        public TableauxNode ParentNode;
        public TableauxNode LeftNode;
        public TableauxNode RightNode;
        public bool Closed = false;

        //Properties
        public bool? branched { get; private set; } = null;
        public int NodeNumber { get; set; } = ++ParsingModule.NodeCounter;


        //Constructors
        public TableauxNode(Component root, TableauxNode parent = null)
        {
            Components = new List<Component>();
            this.ParentNode = parent;
            root.Belongs = this;
            Components.Add(root);
            if (parent != null) 
                parent.LeftNode ??= this;
        }
        public TableauxNode(List<Component> components, TableauxNode parent)
        {
            this.Components = new List<Component>();
            parent.LeftNode = this;
            this.ParentNode = parent;
            components.ForEach(x =>
            {
                var newNode = BinaryTree.CloneNode(x, BinaryTree.Object);
                newNode.Belongs = this;
                this.Components.Add(newNode);
            });
        }
        public TableauxNode(Component node, Component processed ,TableauxNode parent)
        {
            this.Components = new List<Component>();
            Components.Add(node);
            parent.Components.ForEach(node =>
            {
                if (node != processed)
                {
                    var newNode = BinaryTree.CloneNode(node, BinaryTree.Object);
                    newNode.Belongs = this;
                    this.Components.Add(newNode);
                }
            });

            this.ParentNode = parent;
            if (parent.LeftNode == null) parent.LeftNode = this;
            else if (parent.RightNode == null) parent.RightNode = this;

        }

        public void Evaluate(TableauxCalculator visitable) => visitable.Visit(this);

        public void SetBranchStatus(bool stat)
        {
            if (branched == null)
            {
                branched = stat;
            }
        }

        public string Label()
        {
            string label = "";
            Components.ForEach(x => {
                InfixFormulaGenerator.Calculator.Calculate(x);
                label += x.InFixFormula + "\n"; });
            return label;
        }

        public string GraphVizFormula()
        {
            string temp ="";
            temp += $"node{NodeNumber} [ label = \"{this.Label()}\" ]";

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
}
