using System.Collections.Generic;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;

namespace LPP.Parsing_BinaryTree
{
    public class TableauxNode
    {
        public bool Closed = false;
        public bool? branched { get; private set; } = null;
        public List<Component> components;

        public TableauxNode ParentNode;
        public TableauxNode LeftNode;
        public TableauxNode RightNode;

        public TableauxNode(Component root, TableauxNode parent = null)
        {
            components = new List<Component>();
            this.ParentNode = parent;
            parent.LeftNode = this;
            root.Belongs = this;
            components.Add(root);
        }

        public TableauxNode(List<Component> components, TableauxNode parent)
        {
            this.components = new List<Component>();
            var bt = new BinaryTree();
            parent.LeftNode = this;
            this.ParentNode = parent;
            components.ForEach(x =>
            {
                x.Belongs = this;
                this.components.Add(BinaryTree.CloneNode(x, bt));
            });
        }


        public void Evaluate(TableauxCalculator visitable) => visitable.Visit(this);

        public void SetBranchStatus(bool stat)
        {
            if (branched == null)
            {
                branched = stat;
            }
        }

        //public string GraphVizFormula()
        //{
        //    string temp = "";
        //    temp += $"node{NodeNumber} [ label = \"{this.InFixFormula}\" ]";
        //    if (LeftNode != null)
        //    {
        //        temp += $"\nnode{NodeNumber} -- node{LeftFormula.NodeNumber}\n";
        //        temp += LeftFormula.GraphVizFormula;
        //    }
        //    if (RightNode != null)
        //    {
        //        temp += $"\nnode{NodeNumber} -- node{Rightformula.NodeNumber}\n";
        //        temp += Rightformula.GraphVizFormula;
        //    }
        //    return temp;
        //}
    }
}
