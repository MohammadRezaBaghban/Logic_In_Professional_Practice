using System.Collections.Generic;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;

namespace LPP.Parsing_BinaryTree
{
    public class TableauxNode
    {
        public bool Closed = false;
        public bool branched = false;
        public List<Component> components;

        public TableauxNode ParentNode;
        public TableauxNode LeftNode;
        public TableauxNode RightNode;

        public TableauxNode(Component root)
        {
            components = new List<Component>();
            components.Add(root);
            root.Belongs = this;
        }

        public TableauxNode(List<Component> components)
        {
            var bt = new BinaryTree();
            this.components = new List<Component>();
            components.ForEach(x =>
            {
                x.Belongs = this;
                this.components.Add(BinaryTree.CloneNode(x, bt));
            });
        }


        public void Evaluate(TableauxCalculator visitable) => visitable.Visit(this);


    }
}
